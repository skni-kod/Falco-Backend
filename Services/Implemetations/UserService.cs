using AutoMapper;
using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public ResponseDTO DeleteUser(UserDTO user)
        {
            logger.LogInformation("Executing DeleteUser method");

            var result = falcoDbContext.Users.SingleOrDefault(u => u.Id == user.Id);

            if (result == null)
            {
                return new ResponseDTO() { Code = 400, Message = $"User with email {user.Email} does not exist in db", Status = "Error" };
            }
            try
            {
                falcoDbContext.Users.Remove(result);
                falcoDbContext.SaveChanges();
            }
            catch (Exception e)
            {

                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error" };
            }
            return new ResponseDTO() { Code = 200, Message = "Delete user in db", Status = "Succes" };
        }

        public ResponseDTO EditUser(UserDTO user)
        {
            logger.LogInformation("Executing DeleteUser method");

            if (falcoDbContext.Users.Where(u => u.Id == user.Id).Count() == 0)
            {
                return new ResponseDTO() {Code = 400, Message = $"User with email {user.Email} does not exist in db", Status = "Error" };
            }
            try
            {
                falcoDbContext.Users.Update(mapper.Map<UserDTO, User>(user));
                falcoDbContext.SaveChanges();
            }
            catch (Exception e)
            {

                return new ResponseDTO() {Code = 400, Message = e.Message, Status = "Error" };
            }
            return new ResponseDTO() { Code = 200, Message = "Edit user in db", Status = "Succes" };
        }

        public IEnumerable<User> GetAllUsers()
        {
            logger.LogInformation("Executing DeleteUser method");

            return falcoDbContext.Users;
        }

        public User GetUserById(int id)
        {
            logger.LogInformation("Executing GetUserById method");

            return falcoDbContext.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}
