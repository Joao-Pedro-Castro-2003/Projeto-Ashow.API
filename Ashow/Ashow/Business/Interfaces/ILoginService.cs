using Ashow.Domain.Model;

namespace Ashow.Business.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(CredenciaisLoginModel credenciais);
    }
}
