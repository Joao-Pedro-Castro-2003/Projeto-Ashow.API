using Ashow.Business.Interfaces;
using Ashow.Domain.Model;
using Ashow.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Ashow.Business
{
    public class LoginBL : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public LoginBL(ITokenService tokenService, IUsuarioService usuarioService, IMapper map)
        {
            _tokenService = tokenService;
            _usuarioService = usuarioService;
            _mapper = map;
        }

        public async Task<string> Login(CredenciaisLoginModel credenciais)
        {
            await _usuarioService.ValidarUsuarioParaLogin(credenciais.Nome, credenciais.Senha);

            var token = _tokenService.GerarToken(credenciais);

            return token;
        }
    }
}
