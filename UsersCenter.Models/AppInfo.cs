using Common;
using System;
using System.Data;

namespace UsersCenter.Models
{
    ///<summary>
    ///类名：AppInfo
    ///作者：曾璐（abcbsy@163.com）
    ///创建日期：2018/12/11 16:26:06
    ///用途说明：数据表Apps的实体类
    ///修改记录：
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
        /// 应用ID
        /// </summary>
        [Field("AppID", FieldDescription = "应用ID", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string AppID { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        [Field("AppName", FieldDescription = "应用名称", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 10, Scale = 0, AllowNull = true, DefaultValue = "")]
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
        /// APP类型（0：WEB，1：IOS，2：Android等）
        /// </summary>
        [Field("AppType", FieldDescription = "APP类型（0：WEB，1：IOS，2：Android等）", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 10, Scale = 0, AllowNull = true, DefaultValue = "")]
        public int? AppType { get; set; }
        /// <summary>
        /// 所属机构ID
        /// </summary>
        [Field("OrganizationID", FieldDescription = "所属机构ID", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 10, Scale = 0, AllowNull = true, DefaultValue = "")]
        public int? OrganizationID { get; set; }
        /// <summary>
        /// 是否忽略认证
        /// </summary>
        [Field("IgnoreAuthentication", FieldDescription = "是否忽略认证", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 1, Scale = 0, AllowNull = true, DefaultValue = "")]
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