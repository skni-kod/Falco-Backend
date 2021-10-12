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

        public async Task<ConversationInfoDto> AddConversation(Conversation conversation)
        {
            logger.LogInformation("Executing AddConversation method");

            falcoDbContext.Conversations.Add(conversation);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<ConversationInfoDto>(conversation); ;
        }

        public async Task<ConversationInfoDto> DeleteConversation(int conversationID)
        {
            logger.LogInformation("Executing DeleteConversation method");

            var conversation = await falcoDbContext.Conversations
                .SingleOrDefaultAsync(u => u.ConverastionId == conversationID);
            falcoDbContext.Conversations.Remove(conversation);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<ConversationInfoDto>(conversation);
        }
        

        public async Task<ConversationInfoDto> EditConversation(int conversationID, ICollection<User> users)
        {
            logger.LogInformation("Executing EditConversation method");

            Conversation conversation = await falcoDbContext.Conversations
                .SingleOrDefaultAsync(x => x.ConverastionId == conversationID);

            conversation.Owners = users;
            falcoDbContext.Conversations.Update(conversation);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<ConversationInfoDto>(conversation); ;
        }

        public async Task<IEnumerable<ConversationInfoDto>> GetAllConversations()
        {
            logger.LogInformation("Executing GetAllConversations method");

            var conversations = await falcoDbContext.Conversations
                .Select(x => new ConversationInfoDto
                {
                    ConverastionId = x.ConverastionId,
                    Messages = x.Messages.Select(m => new MessageDTO
                    {
                        Message_id = m.Message_id,
                        Author_id = m.Author_id,
                        Conversation_id = m.Conversation_id,
                        Content = m.Content,
                        CreateDate = m.CreateDate,
                    }
                    ),
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
                    ConverastionId = x.ConverastionId,
                    Messages = x.Messages.Select(m => new MessageDTO
                    {
                        Message_id = m.Message_id,
                        Author_id = m.Author_id,
                        Conversation_id = m.Conversation_id,
                        Content = m.Content,
                        CreateDate = m.CreateDate,
                    }
                    ),
                    Owners = x.Owners.Select(o => new UserInfoDto
                    {
                        Id = o.Id,
                        FirstName = o.FirstName,
                        LastName = o.LastName
                    }),
                })
                .SingleOrDefaultAsync(x => x.ConverastionId == conversationID);

            return  conversation;
        }
    }
}
