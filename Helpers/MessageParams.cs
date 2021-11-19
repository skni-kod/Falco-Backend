using API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.Helpers
{
    public class MessageParams : PaginationParams
    {
        public int conversationID { get; set; }
    }
}
