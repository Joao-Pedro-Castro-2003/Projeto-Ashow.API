using Ashow.Domain.Model;

namespace Ashow.Business.Interfaces
{
    public interface IUsuarioService
    {
        Task Cadastrar(UsuarioModel usuario);
        Task Deletar(int id);
        Task Atualizar(UsuarioModel usuario);
        Task<UsuarioModel> BuscarPorId(int id);
        Task ValidarUsuarioParaLogin(string nome, string senha);
    }
}
