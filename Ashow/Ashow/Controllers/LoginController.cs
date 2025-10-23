using Ashow.Business;
using Ashow.Business.Interfaces;
using Ashow.Data;
using Ashow.Domain.Model;
using Ashow.Interface;
using Ashow.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ashow.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService, IMapper map)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(CredenciaisLoginModel credenciais)
        {
            try
            {
                var token = await _loginService.Login(credenciais);

                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true, // 🔐 Impede leitura via JS
                    Secure = true, // 🌐 Só permite HTTPS
                    SameSite = SameSiteMode.None, // 🔒 Diz se front e back estão em domínios diferentes ou não
                    Expires = DateTimeOffset.UtcNow.AddHours(2) // ⏳ Tempo de Expiração
                });

                return Ok("Login realizado com sucesso!");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
