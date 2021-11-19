using FalcoBackEnd.Models.Configuration;
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

        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new ConversationConfiguration());
            //modelBuilder.ApplyConfiguration(new MessageConfiguration());
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>().HasQueryFilter(b => !b.IsDeleted);

            modelBuilder.Entity<Conversation>()
                .HasKey(k => k.ConverastionId);

            modelBuilder.Entity<Message>()
                .HasKey(k => k.Message_id);

            modelBuilder.Entity<UserConversation>()
                .HasKey(uc => new { uc.UserId, uc.ConversationId });
            modelBuilder.Entity<UserConversation>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.Conversations)
                .HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserConversation>()
                .HasOne(uc => uc.Conversation)
                .WithMany(c => c.Owners)
                .HasForeignKey(uc => uc.ConversationId);
        }

    }
}

