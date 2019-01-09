using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersCenter.Api.Models;
using UsersCenter.Services;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        [HttpGet]
        [Route("Logout")]
        public ApiResult Logout()
        {
            SecurityHelper.Logout();
            return EnumApiStatus.BizOK.ToApiResult(null, "退出成功！");
        }
        /// <summary>
        /// 登录接口
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public ApiResult Login([FromBody]UserLoginDto request)
        {
            var ticket = SecurityHelper.Login(request);
            return EnumApiStatus.BizOK.ToApiResult(ticket, "登录成功！");
        }

        /// <summary>
        /// 验证当前是否登录
        /// </summary>
        [HttpGet]
        [Route("ValidateTicket")]
        public bool ValidateTicket(string userName, string userAuthTicket)
        {
            if (string.IsNullOrEmpty(userName)) return false;
            if (string.IsNullOrEmpty(userAuthTicket)) return false;

            var info = userName.FromCache<string>();
            if (info == null || string.IsNullOrEmpty(info.ToString())) return false;
            if (!info.ToString().Equals(userAuthTicket)) return false;

            return true;
        }
    }
}