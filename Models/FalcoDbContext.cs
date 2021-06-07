using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Models
{
    public class FalcoDbContext : DbContext
    {
        public FalcoDbContext(DbContextOptions<FalcoDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
