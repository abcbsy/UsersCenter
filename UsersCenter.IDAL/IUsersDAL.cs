using System;
using System.Data;
using System.Collections.Generic;
using UsersCenter.Models;

namespace UsersCenter.IDAL
{
    ///<summary>
    ///�ӿ�����IUsersDAL
    ///��˾���ƣ�V-Life
    ///���ߣ���责�abcbsy@163.com��
    ///�������ڣ�2018/12/04 13:49:02
    ///��;˵�������ݱ�Users�ķ�������
    ///�޸ļ�¼��
    ///</summary>
    public interface IUsersDAL
	{
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="objWhere"></param>
        /// <param name="order"></param>
        /// <param name="curPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        DataTable Search(UserInfo objWhere, string order, int curPage, int pageSize, out int recordCount, out int pageCount);
	}
}