using Ashow.Business.Interfaces;
using Ashow.Data.Repository;
using Ashow.Domain.Entity;
using Ashow.Domain.Model;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Ashow.Business
{
    public class UsuarioBL : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper mapper;

        public UsuarioBL(IUsuarioRepository usuarioRepository, IMapper map)
        {
            _usuarioRepository = usuarioRepository;
            mapper = map;
        }

        public async Task Cadastrar(UsuarioModel usuarioModel)
        {
            usuarioModel.ValidarCamposDeCadastro();

            usuarioModel.TransformaSenhaEmHash();

            var usuarioDB = await _usuarioRepository.BuscaUsuarioPor(usuarioModel.Nome);

            if (usuarioDB != null)
            {
                throw new Exception("Esse nome de Usuário já está sendo utilizado.");
            }

            await _usuarioRepository.Cadastrar(mapper.Map<UsuarioEntity>(usuarioModel));
        }
        public async Task Deletar(int Id)
        {
            await _usuarioRepository.Deletar(Id);
        }
        public async Task Atualizar(UsuarioModel usuario)
        {
            await _usuarioRepository.Atualizar(mapper.Map<UsuarioEntity>(usuario));
        }
        public async Task<UsuarioModel> BuscarPorId(int Id)
        {
            var usuario = await _usuarioRepository.BuscarPorId(Id);
            return usuario == null ? throw new Exception("Usuário não encontrado") : mapper.Map<UsuarioModel>(usuario);
        }
        public async Task ValidarUsuarioParaLogin(string nome, string senha)
        {
            var usuario = await _usuarioRepository.BuscaUsuarioPor(nome) ?? throw new UnauthorizedAccessException("Usuário ou senha inválidos!");

            var hasher = new PasswordHasher<UsuarioModel>();

            var resultado = hasher.VerifyHashedPassword(mapper.Map<UsuarioModel>(usuario), usuario.Senha, senha);

            if (resultado == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Senha incorreta!");
        }   
    }
}
