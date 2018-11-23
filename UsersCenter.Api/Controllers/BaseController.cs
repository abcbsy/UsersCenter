using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UsersCenter.Api.Models;
using UsersCenter.Models;
using UsersCenter.Services;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Api.Controllers
{
    public class BaseController : Controller
    {
        string GetRequestParam(string paramName)
        {
            if (HttpContext.Request.Headers.ContainsKey(paramName))
            {
                return HttpContext.Request.Headers[paramName].ToString();
            }
            else if (HttpContext.Request.Query.ContainsKey(paramName))
            {
                return HttpContext.Request.Query["x-" + paramName].ToString();
            }
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var auth = GetApiAuthorization(context);
            if (auth.AuthorizationType == EnumAuthorizationType.AllAllow)
            {
                return;
            }
            
            var appId = GetRequestParam("appid");
            var userToken = GetRequestParam("usertoken");
            var appToken = GetRequestParam("apptoken");
            var nonceStr = GetRequestParam("noncestr");
            var encryptUserId = GetRequestParam("userid");
            var sign = GetRequestParam("sign");//签名            
            var timestamp = GetRequestParam("timestamp");//时间戳
            var IgnoreAuth = GetRequestParam("ignoreauth");//忽略认证

            //APP认证
            if ((auth.AuthorizationType & EnumAuthorizationType.AppAuthorization) == EnumAuthorizationType.AppAuthorization)
            {
                var result = OnAppAuthorization(appToken, nonceStr, sign, userToken, encryptUserId, appId, IgnoreAuth);

                if (result.Status != EnumApiStatus.BizOK)
                {
                    context.Result = new JsonResult(result);
                    return;
                }
                else if (!string.IsNullOrEmpty(nonceStr) && !string.IsNullOrEmpty(appToken))
                {
                    ApiSecurityService.SetNonceStr(nonceStr, appToken);
                    ApiSecurityService.SetAppTokenExpire(appToken);
                }
            }
            //用户认证
            if ((auth.AuthorizationType & EnumAuthorizationType.UserAuthorization) == EnumAuthorizationType.UserAuthorization)
            {

                #region 通过API正常登录，有usertoken的验证方式

                var serverTicket = ApiSecurityService.GetUserTicket(userToken);
                ApiResult userAuthRet = EnumApiStatus.BizOK.ToApiResultForApiStatus();
                //用户是否登录(根据userToken取用户信息)
                if (!ApiSecurityService.CheckUserTicket(serverTicket))
                {
                    userAuthRet = new ApiResult() { Status = EnumApiStatus.ApiUserNotLogin, Msg = "用户未登录" };
                }
                else
                {
                    if(!string.IsNullOrEmpty(auth.Roles))//接口限制角色访问
                    {
                        var roles = auth.Roles.Split(',');
                        var userroles = serverTicket.Roles.Split(',');
                        var intersect = userroles.Intersect(roles);
                        if (intersect == null || intersect.Count() == 0)
                        {
                            userAuthRet = new ApiResult() { Status = EnumApiStatus.ApiUserUnauthorized, Msg = "用户无权限访问" };
                        }
                    }
                }
                #endregion

                if (userAuthRet.Status != EnumApiStatus.BizOK)
                {
                    context.Result = new JsonResult(userAuthRet);
                    return;
                }
                else
                {
                    //存入通过认证的登录用户信息
                    HttpContext.Items["LoginUser"] = serverTicket;
                    ApiSecurityService.SetUserTokenExpire(userToken);
                }
            }
        }
        public ApiResult OnAppAuthorization(string appTokenStr, string nonceStr, string sign, string userToken = "", string encryptUserId = "", string appId = "", string IgnoreAuth = "")
        {
            TokenDto token = null;

            if (!string.IsNullOrEmpty(appTokenStr))
            {
                token = ApiSecurityService.GetAppToken(appTokenStr);
            }

            #region 需要忽略验证(测试或压力测试时会出现)
            if (!string.IsNullOrEmpty(IgnoreAuth))
            {
                #region 根据appToken判断是否忽略api接口验证

                if (token != null && ApiSecurityService.IsIgnoreAuthByAppToken(token))
                {
                    HttpContext.Items["AppToken"] = token;
                    return EnumApiStatus.BizOK.ToApiResultForApiStatus();

                }
                #endregion

                #region 根据appId判断是否忽略api接口验证
                if (!string.IsNullOrEmpty(appId) && ApiSecurityService.IsIgnoreAuthByAppId(appId))
                {
                    var appClient = ApiSecurityService.GetAppClientByAppID(appId);
                    var appToken = ApiSecurityService.CreateAppToken(Guid.NewGuid().ToString("N"), appClient);
                    HttpContext.Items["AppToken"] = appToken;
                    return EnumApiStatus.BizOK.ToApiResultForApiStatus();

                }
                #endregion
            }
            #endregion

            if (!ApiSecurityService.CheckAppToken(token))
            {
                return EnumApiStatus.ApiParamAppTokenExpire.ToApiResultForApiStatus("apptoken过期或无效，请重新获取");
            }
            else if (!ApiSecurityService.CheckNonceStr(nonceStr, appTokenStr))
            {
                return EnumApiStatus.ApiRepeatedAccess.ToApiResultForApiStatus("非法请求(重复请求)");
            }
            else if (!ApiSecurityService.CheckSign(token.AppKey, sign, appTokenStr, nonceStr, userToken, encryptUserId))
            {
                return EnumApiStatus.ApiParamSignError.ToApiResultForApiStatus("非法请求(签名错误)");
            }
            else
            {
                //存入通过认证的接入用户信息
                HttpContext.Items["AppToken"] = token;
                return EnumApiStatus.BizOK.ToApiResultForApiStatus();
            }
        }
        
        ApiAuthorization GetApiAuthorization(ActionExecutingContext context)
        {
            ApiAuthorization ret = new ApiAuthorization { AuthorizationType = EnumAuthorizationType.UserAuthorization | EnumAuthorizationType.AppAuthorization, Roles = "1,2,3" };
            return ret;
        }
    }
}