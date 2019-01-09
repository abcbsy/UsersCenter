using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersCenter.Models;

namespace UsersCenter.Services.DTOs
{
    public class UserTicketDto
    {
        #region 用户信息
        /// <summary>
        /// 用户编号
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string PhotoUrl { get; set; }

        #endregion


        public string UserToken { get; set; }
        public EnumUserType UserType { get; set; }

        public string Roles { get; set; }
    }
}
