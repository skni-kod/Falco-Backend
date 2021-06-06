using FalcoBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
