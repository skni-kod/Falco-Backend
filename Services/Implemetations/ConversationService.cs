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

        public async Task<ConversationInfoDTO> AddConversation(ICollection<AddConversationDTO> users)
        {
            var conversation = new Conversation {
                Owners = users.Select(x => new UserConversation
                {
                    UserId = x.Id
                }).ToList()
            };
            //foreach (var x in users)
            //{
            //    conversation.Owners.Add(new UserConversation() { UserId = x.Id });
            //}
            logger.LogInformation("Executing AddConversation method");
            falcoDbContext.Conversations.Add(conversation);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<ConversationInfoDTO>(conversation);
         }

        public async Task<ConversationInfoDTO> DeleteConversation(int conversationID)
        {
            logger.LogInformation("Executing DeleteConversation method");

            var conversation = await falcoDbContext.Conversations
                .SingleOrDefaultAsync(u => u.ConverastionId == conversationID);

            falcoDbContext.Conversations.Remove(conversation);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<ConversationInfoDTO>(conversation);
        }
        

        public async Task<ConversationInfoDTO> EditConversation(int id, ICollection<int> users)
        {
            logger.LogInformation("Executing EditConversation method");

            Conversation conversation = await falcoDbContext.Conversations
                .SingleOrDefaultAsync(x => x.ConverastionId == id);

            falcoDbContext.Conversations.Update(conversation);
            await falcoDbContext.SaveChangesAsync();

            return mapper.Map<ConversationInfoDTO>(conversation); ;
        }

        public async Task<IEnumerable<ConversationInfoDTO>> GetAllConversations()
        {
            logger.LogInformation("Executing GetAllConversations method");

            var conversations = await falcoDbContext.Conversations
                .Select(x => new ConversationInfoDTO
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
                    //Owners = x.Owners.Select(o => new UserInfoDTO
                    //{
                    //    Id = o.Id,
                    //    FirstName = o.FirstName,
                    //    LastName = o.LastName
                    //}),

                    //Owners = x.Owners.Select(users => new UserInfoDTO
                    //{
                    //    Id = users.User.Id,
                    //    FirstName = users.User.FirstName,
                    //    LastName = users.User.LastName
                    //}),
                    Owners = x.Owners.Select(userConversation => new UserConversation{
                          UserId = userConversation.UserId,
                          ConversationId = userConversation.ConversationId,
                    }),
                })
                .AsNoTracking()
                .ToListAsync();

            return conversations;
        }

        public async Task<ConversationInfoDTO> GetConversationByID(int conversationID)
        {
            logger.LogInformation("Executing GetConveration method");

            ConversationInfoDTO conversation = await falcoDbContext.Conversations
                .Select(x => new ConversationInfoDTO
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
                    //Owners = x.Owners.Select(o => new UserInfoDTO
                    //{
                    //    Id = o.Id,
                    //    FirstName = o.FirstName,
                    //    LastName = o.LastName
                    //}),

                    //Owners = x.Owners.Select(users => new UserInfoDTO
                    //{
                    //    Id = users.User.Id,
                    //    FirstName = users.User.FirstName,
                    //    LastName = users.User.LastName
                    //}),
                    Owners = x.Owners.Select(userConversation => new UserConversation
                    {
                        UserId = userConversation.UserId,
                    }),
                })
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ConverastionId == conversationID);

            return  conversation;
        }
    }
}
