using KMRecipePlatform.Models;
using System;
using UsersCenter.Models;

namespace UsersCenter.Api.Filters
{

    /// <summary>
    /// 登录用户的权限认证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UserAuthenticateAttribute : Attribute
    {
        public UserAuthenticateAttribute()
        {
            UserType = EnumUserType.Unknown;
            IsValidUserType = true;
        }

        /// <summary>
        /// 是否验证角色
        /// </summary>
        public bool IsValidUserType { get; set; }


        /// <summary>
        /// 用户类型，患者或医生
        /// </summary>
        public EnumUserType UserType { get; set; }
    }
}