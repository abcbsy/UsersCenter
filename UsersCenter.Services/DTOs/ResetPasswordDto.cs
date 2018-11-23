using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersCenter.Services.DTOs
{
    /// <summary>
    /// 重置密码
    /// </summary>
    public class ResetPasswordDto
    {
        public string UserID { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string ComPassword { get; set; }
    }

    public class RequestUserChangePasswordDTO
    {
        public string UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
