using FalcoBackEnd.Models;
using FalcoBackEnd.Services.Interfaces;
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

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User GetById(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }
    }
}
