using AutoMapper;
using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using FalcoBackEnd.Services.Interfaces;
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

        public ResponseDTO AddConversation(int[] owners)
        {
            logger.LogInformation("Executing AddConversation method");

            if (owners.Length < 1) return new ResponseDTO() { Code = 400, Message = "You must provide a list of owners", Status = "Error" };

            ConversationDTO conversation = new ConversationDTO(owners);

            try
            {
                falcoDbContext.Conversations.Add(mapper.Map<ConversationDTO, Conversation>(conversation));
                falcoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Failed" };
            }
            return new ResponseDTO() { Code = 200, Message = "Added conversation to Db", Status = "Succes" };
        }

        public ResponseDTO DeleteConversation(int conversationID)
        {
            logger.LogInformation("Executing DeleteConversation method");

            var result = falcoDbContext.Conversations.SingleOrDefault(u => u.Converastion_id == conversationID);

            if (result == null)
            {
                return new ResponseDTO() { Code = 400, Message = $"Conversation with id {conversationID} does not exist in db", Status = "Error" };
            }

            try
            {
                falcoDbContext.Conversations.Remove(result);
                falcoDbContext.SaveChanges();
            }
            catch (Exception e)
            {

                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error" };
            }
            return new ResponseDTO() { Code = 200, Message = "Delete user in db", Status = "Success" };
        }

        public ResponseDTO EditConversation(int conversationID, int[] owners)
        {
            logger.LogInformation("Executing EditConversation method");

            if (owners.Length < 1) return new ResponseDTO() { Code = 400, Message = "You must provide a list of owners", Status = "Error" };

            ConversationDTO conversation = new ConversationDTO(owners) {Converastion_id = conversationID};

            if (!falcoDbContext.Conversations.Where(x => x.Converastion_id == conversationID).Any())
            {
                return new ResponseDTO() { Code = 400, Message = $"Conversation with id {conversationID} does not exist in db", Status = "Error" };
            }

            try
            {
                falcoDbContext.Conversations.Update(mapper.Map<ConversationDTO, Conversation>(conversation));
                falcoDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return new ResponseDTO() { Code = 400, Message = e.Message, Status = "Error" };
            }
            return new ResponseDTO() { Code = 200, Message = "Edited conversation in db", Status = "Success" };
        }

        public IEnumerable<Conversation> GetAllConversations()
        {
            logger.LogInformation("Executing GetAllConversations method");

            return falcoDbContext.Conversations;
        }

        public Conversation GetConversationByID(int conversationID)
        {
            logger.LogInformation("Executing GetConveration method");

            return falcoDbContext.Conversations.SingleOrDefault(x => x.Converastion_id == conversationID);
        }
    }
}
