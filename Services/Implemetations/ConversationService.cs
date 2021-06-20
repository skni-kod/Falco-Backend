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

        public ResponseDTO AddConversation(params int[] owners)
        {
            logger.LogInformation("Executing AddConversation method");

            if (owners.Length < 1) return new ResponseDTO() { Code = 400, Message = "You must provide a list of owners", Status = "Error" };

            string ownersString = "";
            foreach (int owner in owners)
            {
                ownersString += owner.ToString() + " ";
            }

            ConversationDTO conversation = new ConversationDTO() { Owners = ownersString };

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

        public Conversation GetConversationByID(int conversationID)
        {
            logger.LogInformation("Executing GetConveration method");

            return falcoDbContext.Conversations.SingleOrDefault(x => x.Converastion_id == conversationID);
        }
    }
}
