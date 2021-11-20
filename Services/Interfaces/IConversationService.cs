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
        Task<ConversationInfoDTO> GetConversationById(int conversationId);
        Task<IEnumerable<ConversationInfoDTO>> GetAllConversations(int userId);
        Task<ConversationInfoDTO> AddConversation(ICollection<AddConversationDTO> users);
        Task<ConversationInfoDTO> EditConversation(int id, ICollection<AddConversationDTO> users);
        Task<ConversationInfoDTO> DeleteConversation(int conversationId);
    }
}
