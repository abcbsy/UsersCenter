using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersCenter.Services.DTOs
{
    public class AppDto
    {
        public string AppID { get; set; }

        public string AppSecret { get; set; }
        
        public string AppKey { get; set; }

        /// <summary>
        /// JS，IOS，Android等
        /// </summary>
        public int AppType { get; set; }

        public int OrganizationID { get; set; }

        public bool? IgnoreAuthentication { get; set; }
    }
}
