using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMRecipePlatform.Models
{
    public class AppClient
    {
        public string AppID { get; set; }

        public string AppSecret { get; set; }
        
        public string AppKey { get; set; }

        /// <summary>
        /// JS，IOS，Android等
        /// </summary>
        public string ClientType { get; set; }

        public string OrgID { get; set; }

        public bool? IgnoreApiAuth { get; set; }
    }
}
