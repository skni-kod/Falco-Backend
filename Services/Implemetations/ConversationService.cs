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
using static FalcoBackEnd.ModelsDTO.ConversationInfoDTO;

namespace FalcoBackEnd.Services.Implemetations
{
    public class ConversationService : IConversationService
    {
        private readonly ILogger logger;
        private readonly IMapper _mapper;
        private readonly FalcoDbContext falcoDbContext;
        
        public ConversationService(ILogger<ConversationService> logger,
                                    IMapper mapper,
                                    FalcoDbContext falcoDbContext)
        {
            this.logger = logger;
            _mapper = mapper;
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

            logger.LogInformation("Executing AddConversation method");
            falcoDbContext.Conversations.Add(conversation);
            await falcoDbContext.SaveChangesAsync();

            if (conversation.ConverastionId == 0) return null;

            var conversationToReturn = _mapper.Map<ConversationInfoDTO>(conversation);

            return conversationToReturn;
         }

        public async Task<ConversationInfoDTO> DeleteConversation(int conversationID)
        {
            logger.LogInformation("Executing DeleteConversation method");

            var conversation = await falcoDbContext.Conversations
                .Include(x => x.Owners)
                .SingleOrDefaultAsync(u => u.ConverastionId == conversationID);

            var conversationToReturn = _mapper.Map<ConversationInfoDTO>(conversation);

            falcoDbContext.Conversations.Remove(conversation);
            await falcoDbContext.SaveChangesAsync();

            return conversationToReturn;
        }
        

        public async Task<ConversationInfoDTO> EditConversation(int id, ICollection<AddConversationDTO> users)
        {
            logger.LogInformation("Executing EditConversation method");      

            Conversation conversation = await falcoDbContext.Conversations
                .Select(x => new Conversation
                {
                    ConverastionId = x.ConverastionId,
                    Owners = x.Owners.Select(userConversation => new UserConversation
                    {
                        UserId = userConversation.UserId,
                        ConversationId = userConversation.ConversationId,
                    }).ToList(),
                })
                .SingleOrDefaultAsync(x => x.ConverastionId == id);

            ICollection<UserConversation> owners = users.Select(x => new UserConversation
            {
                UserId = x.Id,
                ConversationId = id
            }).ToList();

            if (conversation.Owners.Any())
            {
                ICollection<UserConversation> oldUsers = conversation.Owners.Where(x => owners.All(i => i.UserId != x.UserId)).ToList();
                ICollection<UserConversation> newUsers = owners.Where(x => conversation.Owners.All(i => i.UserId != x.UserId)).ToList();
                falcoDbContext.UserConversations.AddRange(newUsers);
                falcoDbContext.UserConversations.RemoveRange(oldUsers);
            }
            else
            {
                conversation.Owners = users.Select(x => new UserConversation
                   {
                       UserId = x.Id
                   }).ToList();
            }

            falcoDbContext.Conversations.Update(conversation);
            await falcoDbContext.SaveChangesAsync();

            var conversationToReturn = _mapper.Map<ConversationInfoDTO>(conversation);

            return conversationToReturn; 
        }

        public async Task<IEnumerable<ConversationInfoDTO>> GetAllConversations()
        {
            logger.LogInformation("Executing GetAllConversations method");

            var conversations = await falcoDbContext.Conversations
                .Select(x => new ConversationInfoDTO
                {
                    ConverastionId = x.ConverastionId,
                    Owners = x.Owners.Select(userConversation => new UserConversationDTO
                    {
                          UserId = userConversation.UserId
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
                    Owners = x.Owners.Select(u => new UserConversationDTO
                    {
                        UserId = u.UserId,
                    }),
                })
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ConverastionId == conversationID);

            return  conversation;
        }
    }
}
