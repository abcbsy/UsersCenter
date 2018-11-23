using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersCenter.Services.DTOs
{
    public class BaseRequestDto
    {
        /// <summary>
        /// 省份ID
        /// </summary>
        public string ProvinceID { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public string CityID { get; set; }
        /// <summary>
        /// 地区ID
        /// </summary>
        public string AreaID { get; set; }
        /// <summary>
        /// UserID
        /// </summary>
        public string UserID { get; set; }
    }

    public class SearchRequestDto : BaseRequestDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; } = 10;
    }

    public class RecipeSearchRequestDto : SearchRequestDto
    {
        /// <summary>
        /// 医生ID
        /// </summary>
        public string DoctorID { get; set; }
        /// <summary>
        /// 药师ID
        /// </summary>
        public string PharmacistID { get; set; }
        /// <summary>
        /// 药房ID
        /// </summary>
        public string PharmacyID { get; set; }
    }

    public class PharmacistSearchRequestDto : SearchRequestDto
    {
        /// <summary>
        /// 药房ID
        /// </summary>
        public string PharmacyID { get; set; }
    }

    public class TimeIntervalRequestDto : SearchRequestDto
    {
        DateTime? _BeginDate = null;
        DateTime? _EndDate = null;
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                if(_BeginDate == null)
                {
                    _BeginDate = SqlDateTime.MinValue.Value;
                }
                return _BeginDate.Value;
            }
            set
            {
                _BeginDate = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_EndDate == null)
                {
                    _EndDate = SqlDateTime.MaxValue.Value;
                }
                return _EndDate.Value;
            }
            set
            {
                _EndDate = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999);
            }
        }
    }

    public class PharmacyServiceStatRequestDto : TimeIntervalRequestDto
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 排序方式（0-消费人次，1-处方销量，2-药品销量）
        /// </summary>
        public int OrderBy { get; set; }
    }
    public class DrugSaleStatRequestDto : TimeIntervalRequestDto
    {
        /// <summary>
        /// 药房ID，默认所有药房
        /// </summary>
        public string PharmacyID { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; } = 10;
    }

    public class PharmacyStatRequestDto : TimeIntervalRequestDto
    {
        /// <summary>
        /// 药房ID，默认所有药房
        /// </summary>
        public string PharmacyID { get; set; }

    }

    public class DrugSaleStatByDatePartRequestDto : TimeIntervalRequestDto
    {
        /// <summary>
        /// 药品ID，默认所有药品
        /// </summary>
        public string DrugID { get; set; }

    }
    public class DrugPharmacySaleRequestDto : TimeIntervalRequestDto
    {
        /// <summary>
        /// 药品ID
        /// </summary>
        public string DrugID { get; set; }

    }
}
