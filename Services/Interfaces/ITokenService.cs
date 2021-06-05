using System.Security.Claims;

namespace FalcoBackEnd.Services.Interfaces
{
    public interface ITokenService
    {
        bool Authenticate(string username, string password);
        string NewToken();
    }
}