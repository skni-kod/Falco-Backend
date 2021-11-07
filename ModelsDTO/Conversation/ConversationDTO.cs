using FalcoBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.ModelsDTO
{
    public class ConversationDTO
    {
        public int ConverastionId { get; set; }
        public IEnumerable<User> Owners { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }
}
