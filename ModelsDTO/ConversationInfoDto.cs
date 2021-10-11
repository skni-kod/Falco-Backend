using FalcoBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.ModelsDTO
{
    public class ConversationInfoDto
    {
        public int Converastion_id { get; set; }
        public IEnumerable<UserInfoDto> Owners { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
