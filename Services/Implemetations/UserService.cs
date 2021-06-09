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
        public IEnumerable<User> GetAll()
        {
            return falcoDbContext.Users;
        }

        public User GetById(int id)
        {
            return falcoDbContext.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}
