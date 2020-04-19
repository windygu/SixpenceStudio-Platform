using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.BaseSite.AuthUser
{
    public class LoginResponse
    {
        public bool result { get; set; }
        public string UserName { get; set; }
        public string Ticket { get; set; }
    }
}
