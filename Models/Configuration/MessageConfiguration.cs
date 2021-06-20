using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Models.Configuration 
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(b => b.Message_id);
            builder.HasOne(b => b.Conversation)
                    .WithMany(b => b.Messages)
                    .HasForeignKey(b => b.Conversation_id)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.Property(b => b.Author_id).IsRequired();
            builder.Property(b => b.Content).IsRequired();
            builder.Property(b => b.CreateDate);
        }
    }
}
