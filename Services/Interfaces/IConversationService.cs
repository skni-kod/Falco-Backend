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
        Task<ConversationInfoDto> GetConversationByID(int conversationID);
        Task<IEnumerable<ConversationInfoDto>> GetAllConversations();
        Task<Conversation> AddConversation(Conversation conversation);
        Task<Conversation> EditConversation(int conversationID, ICollection<User> users);
        Task<Conversation> DeleteConversation(int conversationID);
    }
}
