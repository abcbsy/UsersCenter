using Common;
using DALFactory;
using System.Collections.Generic;
using System.Linq;
using UsersCenter.IDAL;
using UsersCenter.Models;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Services
{
    public class AppsService : BLLClass<AppInfo>
    {
        #region 框架的必要代码
        private readonly string DbConnectionName = "DefaultConnection";
        private IAppsDAL dal;
        public AppsService()
        {
            base.InitDAL(this.DbConnectionName);
            dal = DataAccess.CreateExtendDALClass<IAppsDAL>(this.DbConnectionName);

        }
        #endregion
        string AllCacheKey = "AppClients:All";
        public List<AppDto> GetAll()
        {
            var list = this.GetObjectList(new AppInfo { IsDeleted = false });
            List<AppDto> ret  = list.Select(m=> new AppDto { AppID = m.AppID, AppKey = m.AppKey, AppSecret = m.AppSecret, AppType = m.AppType.Value, IgnoreAuthentication = m.IgnoreAuthentication.Value, OrganizationID = m.OrganizationID.Value }).ToList();
            return ret;
        }
        public List<AppDto> GetAllFromCache()
        {
            var list = AllCacheKey.FromCache<List<AppDto>>();
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
