using UsersCenter.Models;
using UsersCenter.Services;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Api
{
    public class SecurityHelper
    {
        //static string cookiePrefix = "";

        static string UserToken
        {
            get
            {
                if (HttpContextHelper.HttpContext != null)
                {
                    var userTokenStr = HttpContextHelper.HttpContext.Request.Headers["usertoken"];
                    return userTokenStr;
                    //if (string.IsNullOrWhiteSpace(userTokenStr))
                    //{
                    //    //获取Cookie
                    //    var authCookie = HttpContextHelper.HttpContext.Request.Cookies[$"{cookiePrefix}usertoken"];

                    //    if (authCookie != null)
                    //    {
                    //        return authCookie;
                    //    }
                    //    else
                    //    {
                    //        authCookie = HttpContextHelper.HttpContext.Request.Cookies[$"{cookiePrefix}usertoken"];

                    //        if (authCookie != null)
                    //        {
                    //            return authCookie;
                    //        }
                    //        else
                    //        {
                    //            return "";
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    return userTokenStr;
                    //}
                }
                else
                {
                    return "";
                }
            }
        }

        static string AppToken
        {
            get
            {
                if (HttpContextHelper.HttpContext != null)
                {
                    var appTokenStr = HttpContextHelper.HttpContext.Request.Headers["apptoken"];
                    return appTokenStr;
                    //if (string.IsNullOrWhiteSpace(appTokenStr))
                    //{
                    //    //获取Cookie
                    //    var authCookie = HttpContextHelper.HttpContext.Request.Cookies[$"{cookiePrefix}apptoken"];

                    //    if (authCookie != null)
                    //    {
                    //        return authCookie;
                    //    }
                    //    else
                    //    {
                    //        authCookie = HttpContextHelper.HttpContext.Request.Cookies[$"{cookiePrefix}apptoken"];

                    //        if (authCookie != null)
                    //        {
                    //            return authCookie;
                    //        }
                    //        else
                    //        {
                    //            return "";
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    return appTokenStr;
                    //}
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public static UserTicketDto LoginUser
        {
            get
            {
                if (HttpContextHelper.HttpContext != null)
                {
                    #region webApi获取当前用户
                    var user = HttpContextHelper.HttpContext.Items["LoginUser"] as UserTicketDto;
                    if (user != null)
                    {
                        return user;
                    }
                    #endregion

                    if (UserToken != "")
                    {
                        user = ApiSecurityService.GetUserTicket(UserToken);

                        if (user != null)
                        {
                            user.UserToken = UserToken;
                            HttpContextHelper.HttpContext.Items["LoginUser"] = user;
                            return user;
                        }
                    }
                }
                return new UserTicketDto() { UserID = "", UserType = EnumUserType.Unknown };
            }
        }

        /// <summary>
        /// 检查是否已经登录        
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin(EnumUserType userType)
        {
            var user = LoginUser;

            if (user != null && user.UserID != "" && user.UserType == userType)  //没有设置用户角色，则通过
                return true;
            return false;
        }

        /// <summary>
        /// 登录&保存会话信息
        /// </summary>
        public static UserTicketDto Login(UserLoginDto user)
        {
            UserService userService = new UserService();
            var ticket = userService.Login(user);
            HttpContextHelper.HttpContext.Items["LoginUser"] = ticket;
            ApiSecurityService.SetUserTicket(ticket);
            return ticket;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public static void Logout()
        {
            HttpContextHelper.HttpContext.Items["LoginUser"] = null;
            ApiSecurityService.RemoveUserToken(UserToken);
        }


        /// <summary>
        /// 当前请求的apptoken信息
        /// </summary>
        /// <returns></returns>
        public static TokenDto GetCurrentAppToken()
        {
            if (HttpContextHelper.HttpContext != null && HttpContextHelper.HttpContext.Items["AppToken"] != null)
                return HttpContextHelper.HttpContext.Items["AppToken"] as TokenDto;

            return null;
        }

    }
}

