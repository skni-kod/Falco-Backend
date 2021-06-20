using FalcoBackEnd.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Interfaces
{
    public interface IConversationService
    {
        ConversationDTO GetConversation(int conversationID);
        ResponseDTO AddConversation(params int[] owners);
    }
}
