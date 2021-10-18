using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfoDTO>> GetAllUsers();
        Task<UserInfoDTO> GetUserById(int id);
        Task<UserDTO> EditUser(UserDTO userDto);
        Task<UserInfoDTO> DeleteUser(int userId);
    }
}
