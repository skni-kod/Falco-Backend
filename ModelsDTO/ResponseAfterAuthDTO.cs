using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.ModelsDTO
{
    public class ResponseAfterAuthDTO : ResponseDTO
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
