using FalcoBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.ModelsDTO
{
    public class ConversationInfoDTO
    {
        public int ConverastionId { get; set; }
        public IEnumerable<UserConversationDTO> Owners { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }

        public class UserConversationDTO
        {
            public int UserId { get; set; }
        }
    }
}
