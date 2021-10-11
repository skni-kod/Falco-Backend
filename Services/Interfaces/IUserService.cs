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
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<ResponseDTO> EditUser(UserDTO userDto);
        Task<ResponseDTO> DeleteUser(UserDTO user);
    }
}
