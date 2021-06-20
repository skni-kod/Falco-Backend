using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Models
{
    public class Conversation
    {
        public int Converastion_id { get; set; }
        public string Owners { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }
}
