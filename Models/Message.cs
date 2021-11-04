using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Models
{
    public class Message
    {
        public int Message_id { get; set; }
        public int Conversation_id { get; set; }
        public Conversation Conversation { get; set; }
        public int Author_id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdited { get; set; }
    }
}
