using FalcoBackEnd.Models;
using FalcoBackEnd.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Interfaces
{
    public interface IConversationService
    {
        Conversation GetConversationByID(int conversationID);
        ResponseDTO AddConversation(int[] owners);
        ResponseDTO EditConversation(ConversationDTO conversation);
        ResponseDTO DeleteConversation(int conversationID);
    }
}
