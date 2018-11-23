using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersCenter.Common;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Services
{
    public class UserService
    {
        string AllCacheKey = "AppClients:All";
        public UserTicketDto Login(UserLoginDto dto)
        {
            UserTicketDto ret = new UserTicketDto();
            ret.UserToken = Guid.NewGuid().ToString("N");
            ret.UserID = ret.UserAccount = ret.UserName = dto.UserAccount;
            ret.Roles = "1,2,3";
            return ret;
        }
    }
}
