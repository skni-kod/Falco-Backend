using FalcoBackEnd.Helpers;
using FalcoBackEnd.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Services.Interfaces
{
    public interface IMessageService
    {
         Task<MessageDTO> CreateMessage(AddMessageDTO addMessageDTO, int conversationID);
         Task<MessageDTO> DeleteMessage(int messageID);
         Task<PagedList<MessageDTO>> GetMessages(MessageParams messageParams);
         Task<MessageDTO> GetMessage(int messageID);
         Task<MessageDTO> UpdateMessage(string messageContent, int messageID);
    }
}
