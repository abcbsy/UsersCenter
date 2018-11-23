using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersCenter.Common;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Services
{
    public class AppClientService
    {
        string AllCacheKey = "AppClients:All";
        public List<AppClientDto> GetAll()
        {
            List<AppClientDto> list = new List<AppClientDto>();
            list.Add(new AppClientDto { AppID="testid", AppKey="testkey", AppSecret = "testsecret", ClientType = "web", IgnoreApiAuth =false, OrgID = "testorg" });
            return list;
        }
        public List<AppClientDto> GetAllFromCache()
        {
            var list = AllCacheKey.FromCache<List<AppClientDto>>();
            if(list == null)
            {
                list = this.GetAll();
                list.ToCache(AllCacheKey);
            }
            return list;
        }
        public void RemoveCache()
        {
            AllCacheKey.RemoveCache();
        }
    }
}
