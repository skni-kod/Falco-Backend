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
        ConversationDTO GetConversationByID(int conversationID);
        IEnumerable<Conversation> GetAllConversations();
        ResponseDTO AddConversation(int[] owners);
        ResponseDTO EditConversation(int conversationID, int[] owners);
        ResponseDTO DeleteConversation(int conversationID);
    }
}
