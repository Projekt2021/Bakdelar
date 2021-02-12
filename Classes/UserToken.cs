using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool AdminAccess { get; set; }
    }
}
