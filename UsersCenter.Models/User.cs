using System;
using System.Collections.Generic;
using System.Text;

namespace UsersCenter.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string UserAccount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public EnumUserType UserType { get; set; }
        public string Mobile { get; set; }
        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string PhotoUrl { get; set; }
        public string ProvinceID { get; set; }
        public string CityID { get; set; }
        public string AreaID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
