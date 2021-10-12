using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.ModelsDTO
{
    public class MessageDTO
    {
        public int Message_id { get; set; }
        public int Conversation_id { get; set; }
        public int Author_id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
