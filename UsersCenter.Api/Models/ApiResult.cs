using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using UsersCenter.Common;

namespace UsersCenter.Api.Models
{
    /// <summary>
    /// API返回消息基类
    /// </summary>
    public class ApiResult
    {
        public ApiResult()
        {
        }

        public ApiResult(EnumApiStatus status, string msg)
        {
            this.Status = status;
            this.Msg = msg;
        }

        public ApiResult(Exception ex)
        {
            this.Status = EnumApiStatus.BizError;
        }

        /// <summary>
        /// 接口业务状态
        /// </summary>
        public EnumApiStatus Status { get; set; }

        /// <summary>
        /// 消息状态说明
        /// </summary>
        public string Msg { get; set; }

        public object Data { get; set; }
    }
    public static class ApiResultExtend
    {
        public static ApiResult ToApiResultForApiStatus(this EnumApiStatus status, string Msg = "")
        {
            return new ApiResult
            {
                Msg = string.IsNullOrEmpty(Msg) ? status.GetEnumDescription() : Msg,
                Status = status
            };
        }
        public static ApiResult ToApiResult(this EnumApiStatus status, object data, string Msg = "")
        {
            return new ApiResult
            {
                Msg = string.IsNullOrEmpty(Msg) ? status.GetEnumDescription() : Msg,
                Status = status,
                Data = data
            };
        }

        public static ApiResult ToApiResultForBoolean(this bool obj, string Msg = "")
        {
            var Status = obj ? EnumApiStatus.BizOK : EnumApiStatus.BizError;
            return new ApiResult
            {
                Data = obj ? true : false,
                Msg = Msg == "" ? Status.GetEnumDescription() : Msg,
                Status = Status,
            };
        }
    }

    public enum EnumApiStatus
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        BizOK = 1,

        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("操作失败")]
        BizError = -1,

        /// <summary>
        /// 接口参数签名错误
        /// </summary>
        [Description("接口参数签名错误")]
        ApiParamSignError = -2,

        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        ApiParamTokenError = -3,

        /// <summary>
        /// 接口参数数据验证失败
        /// </summary>
        [Description("接口参数数据验证失败")]
        ApiParamModelValidateError = -4,

        /// <summary>
        /// 接口参数应用签名过期
        /// </summary>
        [Description("接口参数应用签名过期")]
        ApiParamAppTokenExpire = -5,

        /// <summary>
        /// 用户未登录
        /// </summary>
        [Description("用户未登录")]
        ApiUserNotLogin = -6,

        /// <summary>
        /// 用户无权限访问
        /// </summary>
        [Description("用户无权限访问")]
        ApiUserUnauthorized = -7,
        /// <summary>
        /// AppClient无权限访问
        /// </summary>
        [Description("AppClient无权限访问")]
        ApiAppClientUnauthorized = -8,

        /// <summary>
        /// 重复请求
        /// </summary>
        [Description("重复请求")]
        ApiRepeatedAccess = -9,

        /// <summary>
        /// 接口时间戳参数错误
        /// </summary>
        [Description("接口时间戳参数错误")]
        ApiParamTimestampError = -10
    }

}