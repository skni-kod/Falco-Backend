using AutoMapper;
using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Implemetations
{
    public class ConversationService : IConversationService
    {
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly FalcoDbContext falcoDbContext;
        
        public ConversationService(ILogger<ConversationService> logger,
                                    IMapper mapper,
                                    FalcoDbContext falcoDbContext)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.falcoDbContext = falcoDbContext;
        }

        public async Task<Conversation> AddConversation(Conversation conversation)
        {
            logger.LogInformation("Executing AddConversation method");

            await falcoDbContext.Conversations.AddAsync(conversation);
            await falcoDbContext.SaveChangesAsync();

            return conversation;
        }

        public async Task<Conversation> DeleteConversation(int conversationID)
        {
            logger.LogInformation("Executing DeleteConversation method");

            var conversation = await falcoDbContext.Conversations.SingleOrDefaultAsync(u => u.Converastion_id == conversationID);
            falcoDbContext.Conversations.Remove(conversation);
            await falcoDbContext.SaveChangesAsync();

            return conversation;
        }
        

        public async Task<Conversation> EditConversation(int conversationID, ICollection<User> users)
        {
            logger.LogInformation("Executing EditConversation method");

            if (!users.Any()) return null;

            Conversation conversation = await falcoDbContext.Conversations
                .SingleOrDefaultAsync(x => x.Converastion_id == conversationID);

            conversation.Owners = users;
            falcoDbContext.Conversations.Update(conversation);
            await falcoDbContext.SaveChangesAsync();

            return conversation;
        }

        public async Task<IEnumerable<ConversationInfoDto>> GetAllConversations()
        {
            logger.LogInformation("Executing GetAllConversations method");

            var conversations = await falcoDbContext.Conversations
                .Select(x => new ConversationInfoDto
                {
                    Converastion_id = x.Converastion_id,
                    Messages = x.Messages,
                    Owners = x.Owners.Select(o => new UserInfoDto
                    {
                        Id = o.Id,
                        FirstName = o.FirstName,
                        LastName = o.LastName
                    }),
                })
                .ToListAsync();

            return conversations;
        }

        public async Task<ConversationInfoDto> GetConversationByID(int conversationID)
        {
            logger.LogInformation("Executing GetConveration method");

            ConversationInfoDto conversation = await falcoDbContext.Conversations
                .Select(x => new ConversationInfoDto
                {
                    Converastion_id = x.Converastion_id,
                    Messages = x.Messages,
                    Owners = x.Owners.Select(o => new UserInfoDto
                    {
                        Id = o.Id,
                        FirstName = o.FirstName,
                        LastName = o.LastName
                    }),
                })
                .SingleOrDefaultAsync(x => x.Converastion_id == conversationID);


            return  conversation;
        }
    }
}
