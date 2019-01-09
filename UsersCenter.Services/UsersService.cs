using Common;
using DALFactory;
using System;
using System.Data;
using UsersCenter.IDAL;
using UsersCenter.Models;
using UsersCenter.Services.DTOs;

namespace UsersCenter.Services
{
    ///<summary>
    ///类名：UsersService
    ///作者：曾璐（abcbsy@163.com）
    ///创建日期：2018/12/04 13:49:02
    ///用途说明：数据表Users的业务类
    ///修改记录：
    ///</summary>
    public class UsersService : BLLClass<UserInfo>
    {
        #region 框架的必要代码
        private readonly string DbConnectionName = "DefaultConnection";
        private IUsersDAL dal;
        public UsersService()
        {
            base.InitDAL(this.DbConnectionName);
            dal = DataAccess.CreateExtendDALClass<IUsersDAL>(this.DbConnectionName);

        }
        #endregion

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="objWhere"></param>
        /// <param name="order"></param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable Search(UserInfo objWhere, string order, int curPage, int pageSize, out int recordCount, out int pageCount)
        {
            return dal.Search(objWhere, order, curPage, pageSize, out recordCount, out pageCount);
        }

        public UserTicketDto Login(UserLoginDto dto)
        {
            var obj = this.GetObject(new UserInfo() { UserAccount = dto.UserAccount, Password = StringEncrypt.EncryptWithMD5(dto.Password) });
            UserTicketDto ret = new UserTicketDto()
            {
                UserToken = Guid.NewGuid().ToString("N"),
                ID = obj.ID,
                UserAccount = obj.UserAccount,
                UserName = obj.UserName,
                Email = obj.Email,
                Mobile = obj.Mobile,
                PhotoUrl = obj.PhotoUrl,
                UserType = (EnumUserType)(obj.UserType ?? 0)
            };
            ret.Roles = "1,2,3";
            return ret;
        }
    }
}
