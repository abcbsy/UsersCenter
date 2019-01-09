using Common;
using System;
using System.Data;

namespace UsersCenter.Models
{
	///<summary>
	///������UserInfo
	///���ߣ���责�abcbsy@163.com��
	///�������ڣ�2018/12/04 13:49:02
	///��;˵�������ݱ�Users��ʵ����
	///�޸ļ�¼��
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
        /// �˺�
        /// </summary>
        [Field("UserAccount", FieldDescription = "�˺�", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = false, DefaultValue = "")]
        public string UserAccount { get; set; }
        /// <summary>
        /// �������ǳ�
        /// </summary>
        [Field("UserName", FieldDescription = "�������ǳ�", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string UserName { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        [Field("Password", FieldDescription = "����", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 128, Scale = 0, AllowNull = false, DefaultValue = "")]
        public string Password { get; set; }
        /// <summary>
        /// �û�����
        /// </summary>
        [Field("UserType", FieldDescription = "�û�����", FieldType = DbType.Int32, IsIdentity = false, IsPrimaryKey = false, Length = 10, Scale = 0, AllowNull = false, DefaultValue = "")]
        public int? UserType { get; set; }
        /// <summary>
        /// �ֻ���
        /// </summary>
        [Field("Mobile", FieldDescription = "�ֻ���", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 32, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string Mobile { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        [Field("Email", FieldDescription = "��������", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 128, Scale = 0, AllowNull = true, DefaultValue = "")]
        public string Email { get; set; }
        /// <summary>
        /// ͷ��
        /// </summary>
        [Field("PhotoUrl", FieldDescription = "ͷ��", FieldType = DbType.String, IsIdentity = false, IsPrimaryKey = false, Length = 128, Scale = 0, AllowNull = true, DefaultValue = "")]
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