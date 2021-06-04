using System.Security.Claims;

namespace FalcoBackEnd.TokenAuthorization
{
    public interface ITokenManager
    {
        bool Authenticate(string username, string password);
        string NewToken();
        ClaimsPrincipal VerifyToken(string token);
    }
}