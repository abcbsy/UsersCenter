using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersCenter.Api.Models
{
    public class LoginModel
    {
        public string UserAccount { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
