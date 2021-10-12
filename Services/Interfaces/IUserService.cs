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
        Task<UserDTO> AddUser(User user);
        Task<IEnumerable<UserInfoDto>> GetAllUsers();
        Task<UserInfoDto> GetUserById(int id);
        Task<UserDTO> EditUser(UserDTO userDto);
        Task<UserInfoDto> DeleteUser(UserDTO user);
    }
}
