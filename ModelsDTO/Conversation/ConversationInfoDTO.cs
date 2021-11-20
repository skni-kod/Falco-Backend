using FalcoBackEnd.Models;
using System.Collections.Generic;

namespace FalcoBackEnd.ModelsDTO
{
    public class ConversationInfoDTO
    {
        public int ConverastionId { get; set; }
        public IEnumerable<UserInfoDTO> Owners { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }

    }
}
