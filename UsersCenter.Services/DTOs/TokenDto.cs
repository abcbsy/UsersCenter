using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersCenter.Services.DTOs
{
    public class TokenDto
    {
        public DateTime Time { get; set; }
        public string AppID { get; set; }
        public string AppKey { get; set; }
        
        public string OrganizationID { get; set; }

        public TimeSpan ExpireDate { get; set; }

        public string Token { get; set; }

        public string ClientType { get; set; }
    }
}
