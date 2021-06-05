using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using System.Security.Claims;

namespace FalcoBackEnd.Services.Interfaces
{
    public interface ITokenService
    {
        AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model);
        string NewToken(User user);
    }
}