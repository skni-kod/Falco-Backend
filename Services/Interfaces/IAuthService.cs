using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace FalcoBackEnd.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO model);
        Task<string> NewToken(User user);
        Task<AuthenticateResponseDTO> AddUser(UserDTO userDTO);
    }
}