using UsersCenter.Api.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Web;
using UsersCenter.Services;
using UsersCenter.Services.DTOs;
using UsersCenter.Common;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UsersCenter.Api.Filters
{
    public class UserCenterAuthorizeFilter : AuthorizeFilter
    {

        private static AuthorizationPolicy _policy_ = new AuthorizationPolicy(new[] { new DenyAnonymousAuthorizationRequirement() }, new string[] { });
        
        // 实现AuthorizeFilter基类，必须有一个过滤策略，也就是，这里我采用的是最基础的DenyAnonymousAuthorizationRequirement（阻止匿名身份的请求）
        public UserCenterAuthorizeFilter() : base(_policy_) { }

        string GetRequestParam(HttpRequest request, string paramName)
        {
            if (request.Headers.ContainsKey(paramName))
            {
                return request.Headers[paramName].ToString();
            }
            else if (request.Query.ContainsKey(paramName))
            {
                return request.Query["x-" + paramName].ToString();
            }
            return null;
        }
        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await base.OnAuthorizationAsync(context);
            if (!context.HttpContext.User.Identity.IsAuthenticated ||
                context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }
            var auth = GetAuthorization(context);
            if (auth == (int)EnumAuthorizationType.AllAllow)
            {
                return;
            }

            var req = context.HttpContext.Request;
            var appId = GetRequestParam(req, "appid");
            var userToken = GetRequestParam(req, "usertoken");
            var appToken = GetRequestParam(req, "apptoken");
            var nonceStr = GetRequestParam(req, "noncestr");
            var encryptUserId = GetRequestParam(req, "userid");
            var sign = GetRequestParam(req, "sign");//签名            
            var timestamp = GetRequestParam(req, "timestamp");//时间戳
            var IgnoreAuth = GetRequestParam(req, "ignoreauth");//忽略认证

            //APP认证
            if ((auth & (int)EnumAuthorizationType.AppAuthorization) == (int)EnumAuthorizationType.AppAuthorization)
            {
                var result = OnAppAuthorization(context, appToken, nonceStr, sign, userToken, encryptUserId, appId, IgnoreAuth);

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
            if ((auth & (int)EnumAuthorizationType.UserAuthorization) == (int)EnumAuthorizationType.UserAuthorization)
            {
                UserAuthenticateAttribute userAuthenticate = null;
                var OnUserAuthorizationResult = OnUserAuthorization(context, appToken, nonceStr, sign, userToken, encryptUserId, appId, IgnoreAuth, userAuthenticate);

                if (OnUserAuthorizationResult.Status != EnumApiStatus.BizOK)
                {
                    context.Result = new JsonResult(OnUserAuthorizationResult);
                    return;
                }
                else
                {
                    ApiSecurityService.SetUserTokenExpire(userToken);
                }
            }
        }

        public ApiResult OnAppAuthorization(AuthorizationFilterContext context, string appTokenStr, string nonceStr, string sign, string userToken = "", string encryptUserId = "", string appId = "", string IgnoreAuth = "")
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
                    context.HttpContext.Items["AppToken"] = token;
                    return EnumApiStatus.BizOK.ToApiResultForApiStatus();

                }
                #endregion

                #region 根据appId判断是否忽略api接口验证
                if (!string.IsNullOrEmpty(appId) && ApiSecurityService.IsIgnoreAuthByAppId(appId))
                {
                    var appClient = ApiSecurityService.GetAppClientByAppID(appId);
                    var appToken = ApiSecurityService.CreateAppToken(Guid.NewGuid().ToString("N"), appClient);
                    context.HttpContext.Items["AppToken"] = appToken;
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
                context.HttpContext.Items["AppToken"] = token;
                return EnumApiStatus.BizOK.ToApiResultForApiStatus();
            }
        }


        public ApiResult OnUserAuthorization(AuthorizationFilterContext context, string appTokenStr, string nonceStr, string sign, string userToken, string encryptUserId, string appId, string IgnoreAuth = "", UserAuthenticateAttribute userAuthenticateAttribute = null)
        {
            #region 通过API正常登录，有usertoken的验证方式
            var serverTicket = ApiSecurityService.GetUserTicket(userToken);

            //用户是否登录(根据userToken取用户信息)
            if (!ApiSecurityService.CheckUserTicket(serverTicket))
            {
                return new ApiResult() { Status = EnumApiStatus.ApiUserNotLogin, Msg = "用户未登录" };
            }
            else
            {
                if (userAuthenticateAttribute != null && userAuthenticateAttribute.IsValidUserType && serverTicket.UserType != userAuthenticateAttribute.UserType)
                {
                    return new ApiResult() { Status = EnumApiStatus.ApiUserUnauthorized, Msg = "用户无权限访问" };
                }
                else
                {
                    //存入通过认证的登录用户信息
                    context.HttpContext.Items["LoginUser"] = serverTicket;
                    return EnumApiStatus.BizOK.ToApiResultForApiStatus();
                }
            }

            #endregion
        }

        int GetAuthorization(AuthorizationFilterContext context)
        {
            return 0;
        }
    }


}