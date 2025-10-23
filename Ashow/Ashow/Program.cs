using Ashow.Business;
using Ashow.Business.Interfaces;
using Ashow.Data;
using Ashow.Data.Repository;
using Ashow.Domain.Entity;
using Ashow.Domain.Model;
using Ashow.Interface;
using Ashow.Service;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ativa o mapeamento snake_case → PascalCase no Dapper
DefaultTypeMap.MatchNamesWithUnderscores = true;

#region JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };

        // 👇 Adiciona suporte a JWT via cookie
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("jwt"))
                {
                    context.Token = context.Request.Cookies["jwt"];
                }
                return Task.CompletedTask;
            }
        };
    });
#endregion

#region :: AutoMapper ::
var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<FilmeEntity, FilmeModel>().ReverseMap();
    cfg.CreateMap<UsuarioEntity, UsuarioModel>().ReverseMap();
    cfg.CreateMap<SerieEntity, SerieModel>().ReverseMap();
    cfg.CreateMap<GeneroEntity, GeneroModel>().ReverseMap();
    cfg.CreateMap<CredenciaisLoginEntity, CredenciaisLoginModel>().ReverseMap();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Dapper
// Configure Dapper
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion


#region AddScoped
//Prefira sempre registrar só interfaces
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginService, LoginBL>();
builder.Services.AddScoped<IUsuarioService, UsuarioBL>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioDAO>();
builder.Services.AddScoped<IAshowService, AshowBL>();
builder.Services.AddScoped<IAshowRepository, AshowDAO>();
#endregion

#region AddTransient
#endregion

#region LifeTimes

#region Transient 
/*
    Um novo objeto é criado toda vez que o serviço for solicitado.
    Ideal para serviços leves e sem estado.
    Bom para: validações, helpers, serviços utilitários.
    Evite quando ele depende de algo com estado (como DbContext).
*/
#endregion

#region Scoped 
/*
    Uma instância por requisição HTTP.
    Ideal para camada de negócio (BL) e DAO/Repository, porque durante a mesma requisição todos compartilham o mesmo contexto/transação.
*/
#endregion

#region Singleton  
/*
    Uma única instância para toda a aplicação.
    Usado para cache, configuração, logging, etc.
*/
#endregion

#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirReact", policy =>
    {
        policy.WithOrigins("https://localhost:5173", "http://localhost:5173")   //Só permite essa origem                                                                       
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Define a raiz do Swagger UI
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("PermitirReact");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();