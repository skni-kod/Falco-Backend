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
        Task<ConversationInfoDTO> GetConversationByID(int conversationID);
        Task<IEnumerable<ConversationInfoDTO>> GetAllConversations();
        Task<ConversationInfoDTO> AddConversation(ICollection<AddConversationDTO> users);
        Task<ConversationInfoDTO> EditConversation(int id, ICollection<int> users);
        Task<ConversationInfoDTO> DeleteConversation(int conversationID);
    }
}
