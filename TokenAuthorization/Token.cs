using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FalcoBackEnd.TokenAuthorization
{
    public class Token
    {
        public String Value { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}
