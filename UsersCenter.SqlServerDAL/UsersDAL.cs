using System;
using System.Data;
using System.Collections.Generic;
using UsersCenter.IDAL;
using UsersCenter.Models;
using Microsoft.Extensions.Configuration;
using SqlServerDAL;

namespace UsersCenter.SqlServerDAL
{
    ///<summary>
    ///������UsersDAL
    ///���ߣ���责�abcbsy@163.com��
    ///�������ڣ�2018/12/04 13:49:02
    ///��;˵�������ݱ�Users�ķ�����
    ///�޸ļ�¼��
    ///</summary>
    public class UsersDAL : DALClass<UserInfo>, IUsersDAL
    {
        #region ���캯��&��ܵı�Ҫ����
        public UsersDAL() { }
        public UsersDAL(IConfigurationSection setting) : base(setting)
        {

        }
        #endregion

        public DataTable Search(UserInfo objWhere, string order, int curPage, int pageSize, out int recordCount, out int pageCount)
        {
            System.Text.StringBuilder sbWhere = new System.Text.StringBuilder("1 = 1");
            //TODO
//            if (!string.IsNullOrEmpty(objWhere.Name) && objWhere.Name.Trim() != "")
//            {
//                sbWhere.AppendFormat(" AND Name LIKE '%{0}%'", DbHelper.SqlAttackTrim(objWhere.Name));
//            }
            return this.GetPage(pageSize, curPage, "*", "Users", sbWhere.ToString(), order, out recordCount, out pageCount); 
        }
    }
}