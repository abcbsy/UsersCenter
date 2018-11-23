using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersCenter.Models
{
    public enum EnumUserType
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        [Description("普通用户")]
        Unknown = 0,
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        SuperAdmin = 99
    }

    public enum EnumAuthorizationType
    {
        AllAllow = 0,
        AppAuthorization = 1,
        UserAuthorization = 2
    }

    public enum EnumIDCardType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        IDCard = 0,

        /// <summary>
        /// 居民户口本
        /// </summary>
        [Description("居民户口本")]
        ResidentsBooklet = 1,

        /// <summary>
        /// 护照
        /// </summary>
        [Description("护照")]
        Passport = 2,

        /// <summary>
        /// 军官证
        /// </summary>
        [Description("军官证")]
        MilitaryOfficer = 3,

        /// <summary>
        /// 驾驶证
        /// </summary>
        [Description("驾驶证")]
        DriverLicense = 4,

        /// <summary>
        /// 港澳通行证
        /// </summary>
        [Description("港澳通行证")]
        HKMacaoPass = 5,

        /// <summary>
        /// 台湾通行证
        /// </summary>
        [Description("台湾通行证")]
        TaiwanPass = 6,

        /// <summary>
        /// 港澳台身份证
        /// </summary>
        [Description("港澳台身份证")]
        HMTIDCard = 7,

        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other = 99


    }

    public enum EnumGender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 0,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 1,
        ///// <summary>
        ///// 其他
        ///// </summary>
        [Description("未知")]
        Other = 2
    }
}
