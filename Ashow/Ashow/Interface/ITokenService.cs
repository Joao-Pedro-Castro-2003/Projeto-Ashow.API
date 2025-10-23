using Ashow.Domain.Model;

namespace Ashow.Interface
{
    public interface ITokenService
    {
        string GerarToken(CredenciaisLoginModel credenciais);
    }
}
