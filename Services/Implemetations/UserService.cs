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
        private List<User> users = new List<User>
        {
            new User {Id = 0, FirstName="Horse", LastName="Boar", Email="admin@gmail.com", Password="password"}
        };

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

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User GetById(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public ResponseDTO AddUser(User user)
        {
            try
            {
                falcoDbContext.Users.Add(user);
                falcoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Failed" };
            }
            return new ResponseDTO() { Code = 200, Message = "Added user to DB", Status = "Success" };
        }
    }
}
