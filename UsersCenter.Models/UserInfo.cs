using Common;
using System;
using System.Data;

namespace UsersCenter.Models
{
	///<summary>
	///类名：UserInfo
	///作者：曾璐（abcbsy@163.com）
	///创建日期：2018/12/04 13:49:02
	///用途说明：数据表Users的实体类
	///修改记录：
	///</summary>
	[Table("Users")]
	public class UserInfo
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [Field("ID", FieldDescription = "", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = true, Length = 19, Scale = 0, AllowNull = false, DefaultValue = "")]
        public int? ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Field("UserAccount", FieldDescription = "账号", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = false, DefaultValue = "")]
        public string UserAccount { get; set; }
        /// <summary>
        /// 姓名或昵称
        /// </summary>
        [Field("UserName", FieldDescription = "姓名或昵称", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Field("Password", FieldDescription = "密码", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 128, Scale = 0, AllowNull = false, DefaultValue = "")]
        public string Password { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [Field("UserType", FieldDescription = "用户类型", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 10, Scale = 0, AllowNull = false, DefaultValue = "")]
        public int? UserType { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Field("Mobile", FieldDescription = "手机号", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string Mobile { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Field("Email", FieldDescription = "电子邮箱", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 128, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Field("PhotoUrl", FieldDescription = "头像", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 128, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string PhotoUrl { get; set; }
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
        public int? IsDeleted { get; set; }

        #endregion
    }
}