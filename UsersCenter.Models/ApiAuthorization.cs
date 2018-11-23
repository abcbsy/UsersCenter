using System;
using System.Collections.Generic;
using System.Text;

namespace UsersCenter.Models
{
    public class ApiAuthorization
    {
        public string Area { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public EnumAuthorizationType AuthorizationType { get; set; }
        public string Roles { get; set; }

    }
}
