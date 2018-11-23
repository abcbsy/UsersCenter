using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersCenter.Api.Models;
using UsersCenter.Services;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Api.Controllers
{
    [Route("api/[controller]")]
    public class AppClientsController : Controller
    {
        [HttpGet, Route("GetToken")]
        public ApiResult GetToken(string appId, string appSecret)
        {
            var appClient = new AppClientDto();
            if (ApiSecurityService.CheckAppClient(appId, appSecret, out appClient) == false)
            {
                return EnumApiStatus.ApiAppClientUnauthorized.ToApiResultForApiStatus();
            }
            string token = Guid.NewGuid().ToString("N");
            //保存token
            ApiSecurityService.SetAppToken(token, appClient);

            return EnumApiStatus.BizOK.ToApiResult(token);
        }
    }
}
