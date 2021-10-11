using AutoMapper;
using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FalcoBackEnd.Services.Implemetations
{
    public class UserService : IUserService
    {
        private readonly FalcoDbContext falcoDbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public UserService(FalcoDbContext falcoDbContext,
                            ILogger<UserService> logger,
                            IMapper mapper)
        {
            this.falcoDbContext = falcoDbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ResponseDTO> DeleteUser(UserDTO userDto)
        {
            logger.LogInformation("Executing DeleteUser method");

            var result = await falcoDbContext.Users.SingleOrDefaultAsync(u => u.Id == userDto.Id);

            if (result == null)
            {
                return new ResponseDTO() { Code = 400, Message = $"User with email {userDto.Email} does not exist in db", Status = "Error" };
            }
            falcoDbContext.Users.Remove(result);
            await falcoDbContext.SaveChangesAsync();
 
            return new ResponseDTO() { Code = 200, Message = "Delete user in db", Status = "Success" };
        }

        public async Task<ResponseDTO> EditUser(UserDTO userDto)
        {
            logger.LogInformation("Executing DeleteUser method");

            var user = await falcoDbContext.Users.SingleOrDefaultAsync(u => u.Id == userDto.Id);

            if (user != null)
            {
                return new ResponseDTO() {Code = 400, Message = $"User with email {userDto.Email} does not exist in db", Status = "Error" };
            }

            //  We dont wont mapper in Update method
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;


            falcoDbContext.Users.Update(user);
            await falcoDbContext.SaveChangesAsync();

            return new ResponseDTO() { Code = 200, Message = "Edited user in db", Status = "Success" };
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            logger.LogInformation("Executing GetAllUsers method");

            return await falcoDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            logger.LogInformation("Executing GetUserById method");

            return await falcoDbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
