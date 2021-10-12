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

        public async Task<UserDTO> AddUser(User user)
        {
            falcoDbContext.Users.Add(user);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<UserDTO>(user); ;
        }

        public async Task<UserInfoDto> DeleteUser(UserDTO userDto)
        {
            logger.LogInformation("Executing DeleteUser method");

            var result = await falcoDbContext.Users.SingleOrDefaultAsync(u => u.Id == userDto.Id);
            falcoDbContext.Users.Remove(result);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<UserInfoDto>(result);
        }

        public async Task<UserDTO> EditUser(UserDTO userDto)
        {
            logger.LogInformation("Executing DeleteUser method");

            var user = await falcoDbContext.Users.SingleOrDefaultAsync(u => u.Id == userDto.Id);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;

            falcoDbContext.Users.Update(user);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserInfoDto>> GetAllUsers()
        {
            logger.LogInformation("Executing GetAllUsers method");

            var users =  await falcoDbContext.Users
                .Select( u => new UserInfoDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToListAsync();

            return users;
        }

        public async Task<UserInfoDto> GetUserById(int id)
        {
            logger.LogInformation("Executing GetUserById method");

            var user = await falcoDbContext.Users
                .Select(u => new UserInfoDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .SingleOrDefaultAsync(x => x.Id == id);

            return user;
        }
    }
}
