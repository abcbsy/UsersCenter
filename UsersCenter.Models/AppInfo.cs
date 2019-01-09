using Common;
using System;
using System.Data;

namespace UsersCenter.Models
{
    ///<summary>
    ///������AppInfo
    ///���ߣ���责�abcbsy@163.com��
    ///�������ڣ�2018/12/11 16:26:06
    ///��;˵�������ݱ�Apps��ʵ����
    ///�޸ļ�¼��
    ///</summary>
    [Table("Apps")]
    public class AppInfo
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Field("ID", FieldDescription = "", FieldType = DbType.Int32, IsIdentity = true, IsPrimaryKey = true, Length = 19, Scale = 0, AllowNull = false, DefaultValue = "")]
        public int? ID { get; set; }
        /// <summary>
        /// Ӧ��ID
        /// </summary>
        [Field("AppID", FieldDescription = "Ӧ��ID", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string AppID { get; set; }
        /// <summary>
        /// Ӧ������
        /// </summary>
        [Field("AppName", FieldDescription = "Ӧ������", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 10, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string AppName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field("AppKey", FieldDescription = "", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string AppKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field("AppSecret", FieldDescription = "", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string AppSecret { get; set; }
        /// <summary>
        /// APP���ͣ�0��WEB��1��IOS��2��Android�ȣ�
        /// </summary>
        [Field("AppType", FieldDescription = "APP���ͣ�0��WEB��1��IOS��2��Android�ȣ�", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 10, Scale = 0, AllowNull = true, DefaultValue = "")]
        public int? AppType { get; set; }
        /// <summary>
        /// ��������ID
        /// </summary>
        [Field("OrganizationID", FieldDescription = "��������ID", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 10, Scale = 0, AllowNull = true, DefaultValue = "")]
        public int? OrganizationID { get; set; }
        /// <summary>
        /// �Ƿ������֤
        /// </summary>
        [Field("IgnoreAuthentication", FieldDescription = "�Ƿ������֤", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 1, Scale = 0, AllowNull = true, DefaultValue = "")]
        public bool? IgnoreAuthentication { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field("CreateTime", FieldDescription = "", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 23, Scale = 3, AllowNull = false, DefaultValue = "getdate")]
        public string CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field("ModifyTime", FieldDescription = "", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 23, Scale = 3, AllowNull = true, DefaultValue = "getdate")]
        public string ModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Field("IsDeleted", FieldDescription = "", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 1, Scale = 0, AllowNull = false, DefaultValue = "0")]
        public bool? IsDeleted { get; set; }

        #endregion
    }
}