using AutoMapper;
using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var conversation = new Conversation
            {
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

        public async Task<ConversationInfoDTO> DeleteConversation(int conversationId)
        {
            logger.LogInformation("Executing DeleteConversation method");

            var conversation = await falcoDbContext.Conversations
                .Include(x => x.Owners)
                .SingleOrDefaultAsync(u => u.ConverastionId == conversationId);

            var conversationToReturn = _mapper.Map<ConversationInfoDTO>(conversation);

            falcoDbContext.Conversations.Remove(conversation);
            await falcoDbContext.SaveChangesAsync();

            return conversationToReturn;
        }


        public async Task<ConversationInfoDTO> EditConversation(int id, ICollection<AddConversationDTO> users)
        {
            logger.LogInformation("Executing EditConversation method");

            var conversation = await falcoDbContext.Conversations.Include(x => x.Owners).SingleOrDefaultAsync(x => x.ConverastionId == id);

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

        public async Task<IEnumerable<ConversationInfoDTO>> GetAllConversations(int userId)
        {
            logger.LogInformation("Executing GetAllConversations method");

            var conversations = await falcoDbContext.Conversations.Where(x => x.Owners.Any(x => x.UserId == userId))
                .Select(x => new ConversationInfoDTO
                {
                    ConverastionId = x.ConverastionId,
                    Owners = x.Owners.Select(userConversation => new UserInfoDTO
                    {
                         Id = userConversation.UserId,
                         FirstName = userConversation.User.FirstName,
                         LastName = userConversation.User.LastName
                    }),
                })
                .ToListAsync();

            return conversations;
        }

        public async Task<ConversationInfoDTO> GetConversationById(int conversationId)
        {
            logger.LogInformation("Executing GetConveration method");

            ConversationInfoDTO conversation = await falcoDbContext.Conversations.Where(x => x.Owners.Any(x => x.ConversationId == conversationId))
                .Select(x => new ConversationInfoDTO
                {
                    ConverastionId = x.ConverastionId,
                    Owners = x.Owners.Select(u => new UserInfoDTO
                    {
                        Id = u.UserId,
                        FirstName = u.User.FirstName,
                        LastName = u.User.LastName

                    }),
                })
                .SingleOrDefaultAsync();

            return conversation;
        }
    }
}
