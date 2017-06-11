using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benandkatiegetmarried.UseCases.Login
{
    public class GuestLoginRequest
    {
        public string SecurityCode { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
