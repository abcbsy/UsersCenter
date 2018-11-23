using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersCenter.Services.DTOs
{
    public class UserLoginDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///  验证码
        /// </summary>
        public string VerifyCode { get; set; }
    }
}
