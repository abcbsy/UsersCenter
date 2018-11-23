using KMRecipePlatform.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersCenter.Common;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Services
{
    public class ApiSecurityService
    {
        static int TokenCacheSeconds = 3600;
        static string GetNonceStrCacheKey(string appToken, string nonce)
        {
            return $"NonceStr:{appToken}:{nonce}";
        }

        static string GetAppTokenCacheKey(string appToken)
        {
            return $"AppToken:{appToken}";
        }
        static string GetUserTicketCacheKey(string userToken)
        {
            return $"UserTicket:{userToken}";
        }
        static string GetUserTokenCacheKey(string userToken)
        {
            return $"UserToken:{userToken}";
        }

        #region 校验方法


        /// <summary>
        /// app接入端是否合法
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static bool CheckAppClient(string appId, string appSecret, out AppClientDto appClient)
        {
            appClient = new AppClientDto();

            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appSecret))
            {
                return false;
            }
            //检查id,secret是否有效
            appClient = GetAppClientByAppID(appId);
            if (appClient != null && appClient.AppSecret == appSecret)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool CheckSign(string appkey, string sign, string apptoken, string noncestr, string usertoken = "", string userId = "")
        {
            if (string.IsNullOrEmpty(sign))
            {
                return false;
            }

            //签名参数不固定（参数按照字母排列）
            var signParam = GetSignParam(appkey, apptoken, noncestr, usertoken, userId);

            if (GetSign(signParam) == sign)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public static bool CheckAppToken(string tokenStr)
        {
            if (string.IsNullOrEmpty(tokenStr))
                return false;

            var token = GetAppToken(tokenStr);

            if (token == null)
                return false;


            return true;
        }

        public static bool CheckAppToken(TokenDto token)
        {
            if (token == null)
                return false;

            return true;
        }

        /// <summary>
        /// 检查登录用户
        /// </summary>
        /// <param name="userTokenStr"></param>
        /// <returns></returns>
        public static bool CheckUserTicket(UserTicketDto ticket)
        {
            if (ticket == null || string.IsNullOrEmpty(ticket.UserID))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        static string GetTimestamp()
        {
            //Utc,格林威治的当前时间
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 验证时间戳
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static bool CheckTimestamp(string timeStamp)
        {
            if (string.IsNullOrEmpty(timeStamp))
                return false;

            var reqTimeStamp = Convert.ToInt64(timeStamp);//客户端请求时的时间戳
            var nowTimestamp = Convert.ToInt64(GetTimestamp());//服务器当前时间戳
            var allowSeconds = 5 * 60;//允许时间波动范围,5分钟
            if (reqTimeStamp < nowTimestamp - allowSeconds || reqTimeStamp > nowTimestamp + allowSeconds)
                return false;
            return true;
        }

        /// <summary>
        /// 验证随机数(在指定时间内随机数不能重复)
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckNonceStr(string nonce, string token)
        {
            if (string.IsNullOrEmpty(nonce) || nonce.Length < 10 || nonce.Length > 40)
                return false;

            var NonceStrCacheKey = GetNonceStrCacheKey(token, nonce);
            string cacheNonceStr = NonceStrCacheKey.FromCache<string>();

            if (cacheNonceStr == "1")
                return false;

            return true;
        }

        #endregion

        #region 设置方法

        /// <summary>
        /// 保存token
        /// </summary>
        public static void SetAppToken(string appToken, AppClientDto account)
        {
            var AppTokenCacheKey = GetAppTokenCacheKey(appToken);
            var token = CreateAppToken(appToken, account);
            token.ToCache(AppTokenCacheKey, TokenCacheSeconds);
        }

        /// <summary>
        /// 创建AppToken
        /// </summary>
        /// <param name="appToken"></param>
        /// <param name="appClient"></param>
        /// <returns></returns>
        public static TokenDto CreateAppToken(string appToken, AppClientDto appClient)
        {
            var nowTime = DateTime.Now;
            var token = new TokenDto()
            {
                AppKey = appClient.AppKey,
                Token = appToken,
                AppID = appClient.AppID,
                ClientType = appClient.ClientType,
                Time = nowTime,
                ExpireDate = TimeSpan.FromSeconds(TokenCacheSeconds),
                OrganizationID = appClient.OrgID
            };

            return token;

        }


        /// <summary>
        /// 记录本次请求的随机数
        /// </summary>
        /// <param name="nonceStr"></param>
        /// <param name="tokenStr"></param>
        public static void SetNonceStr(string nonce, string appToken)
        {
            if (!string.IsNullOrWhiteSpace(nonce) && !string.IsNullOrWhiteSpace(appToken))
            {
                var NonceStrCacheKey = GetNonceStrCacheKey(appToken, nonce);
                "1".ToCache(NonceStrCacheKey, 300);
            }
        }


        /// <summary>
        /// 登入，保存用户信息
        /// </summary>
        /// <param name="userTokenStr"></param>
        /// <param name="user"></param>
        public static void SetUserTicket(UserTicketDto user)
        {
            user.ToCache(GetUserTicketCacheKey(user.UserToken), TokenCacheSeconds);
        }

        /// <summary>
        ///设置登录用户的相对过期
        /// </summary>
        public static void SetUserTokenExpire(string userToken)
        {
            if (!string.IsNullOrEmpty(userToken))
            {
                //设置过期时间
                var userTokenCacheKey = GetUserTokenCacheKey(userToken);
                var userTicketCacheKey = GetUserTicketCacheKey(userToken);

                userTokenCacheKey.KeyExpire(TimeSpan.FromSeconds(TokenCacheSeconds));
                userTicketCacheKey.KeyExpire(TimeSpan.FromSeconds(TokenCacheSeconds));
            }
        }

        /// <summary>
        ///设置登录用户的相对过期
        /// </summary>
        public static void SetAppTokenExpire(string appToken)
        {
            if (!string.IsNullOrEmpty(appToken))
            {
                //设置过期时间
                var cacheKey = GetAppTokenCacheKey(appToken);
                cacheKey.KeyExpire(TimeSpan.FromSeconds(TokenCacheSeconds));
            }
        }
        #endregion

        #region 获取方法


        /// <summary>
        /// 获取接入端账户信息
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static AppClientDto GetAppClientByAppID(string appId)
        {

            return new AppClientService().GetAllFromCache().FirstOrDefault(i => i.AppID == appId);
        }

        /// <summary>
        /// 获取验证通过的接入端信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TokenDto GetAppToken(string token)
        {
            var CacheKey = GetAppTokenCacheKey(token);
            return CacheKey.FromCache<TokenDto>();
        }

        /// <summary>
        /// 获取签名参数
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static SortedList GetSignParam(string appkey, string apptoken, string noncestr, string usertoken, string userId = "")
        {

            var signParam = new SortedList();
            signParam.Add("appkey", appkey);
            signParam.Add("apptoken", apptoken);
            signParam.Add("noncestr", noncestr);

            if (!string.IsNullOrEmpty(userId))
            {
                signParam.Add("userid", userId);
            }

            if (!string.IsNullOrEmpty(usertoken))
            {
                signParam.Add("usertoken", usertoken);
            }

            return signParam;
        }
        
        /// <summary>
        /// 获取参数签名
        /// </summary>
        /// <param name="reqParams"></param>
        /// <param name="appkey"></param>
        /// <returns></returns>
        public static string GetSign(SortedList reqParams)
        {
            var sb = new StringBuilder();
            foreach (string key in reqParams.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }

                sb.Append(key + "=" + reqParams[key]);
            }

            return GetSign(sb.ToString());
        }

        public static string GetSign(string str)
        {
            string sign = StringEncrypt.EncryptWithMD5(str, "UTF-8").ToUpper();
            return sign;
        }

        public static UserTicketDto GetUserTicket(string userToken)
        {
            var CacheKey = GetUserTicketCacheKey(userToken);
            var ticket = CacheKey.FromCache<UserTicketDto>();
            if (ticket != null)
            {
                return ticket;

            }
            else
            {
                return default(UserTicketDto);
            }
        }
        
        /// <summary>
        /// 获取接入端密钥
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public static string GetAppKey(string tokenStr)
        {
            var token = GetAppToken(tokenStr);
            if (token != null)
            {
                var account = GetAppClientByAppID(token.AppID);
                if (account != null)
                {
                    return account.AppKey;
                }
            }

            return "";
        }

        /// <summary>
        /// 获取接入端编号
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public static string GetAppId(string tokenStr)
        {
            var token = GetAppToken(tokenStr);
            if (token != null)
            {
                var account = GetAppClientByAppID(token.AppID);
                if (account != null)
                {
                    return account.AppID;
                }
            }

            return "";
        }

        /// <summary>
        /// 是否忽略验证
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public static bool IsIgnoreAuthByAppToken(string appTokenStr)
        {
            var token = GetAppToken(appTokenStr);

            if (token != null)
            {
                var account = GetAppClientByAppID(token.AppID);
                if (account != null)
                {
                    return account.IgnoreApiAuth.HasValue ? account.IgnoreApiAuth.Value : false;
                }
            }

            return false;
        }
        
        /// <summary>
        /// 是否忽略验证
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        public static bool IsIgnoreAuthByAppToken(TokenDto token)
        {
            if (token != null)
            {
                var account = GetAppClientByAppID(token.AppID);
                if (account != null)
                {
                    return account.IgnoreApiAuth.HasValue ? account.IgnoreApiAuth.Value : false;
                }
            }

            return false;
        }

        public static bool IsIgnoreAuthByAppId(string appId)
        {
            var account = GetAppClientByAppID(appId);
            if (account != null)
            {
                return account.IgnoreApiAuth.HasValue ? account.IgnoreApiAuth.Value : false;
            }
            return false;
        }

        #endregion

        public static void RemoveUserToken(string userToken)
        {
            GetUserTicketCacheKey(userToken).RemoveCache();
            GetUserTokenCacheKey(userToken).RemoveCache();
        }
        
    }
}
