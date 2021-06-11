using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.ModelsDTO
{
    public class ResponseDTO
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public int Code { get; set; }
    }
}
