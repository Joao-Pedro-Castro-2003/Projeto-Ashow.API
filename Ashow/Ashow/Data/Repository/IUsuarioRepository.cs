using Ashow.Domain.Entity;
using Ashow.Domain.Model;
using System.Globalization;

namespace Ashow.Data.Repository
{
    public interface IUsuarioRepository
    {
        Task Cadastrar(UsuarioEntity usuario);
        Task Deletar(int id);
        Task Atualizar(UsuarioEntity usuario);
        Task<UsuarioEntity> BuscarPorId(int id);
        Task<UsuarioEntity> BuscaUsuarioPor(string nome);
    }
}
