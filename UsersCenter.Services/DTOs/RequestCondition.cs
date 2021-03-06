﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersCenter.Services.DTOs
{
    public class RequestCondition: BaseRequestDto
    {
        public RequestCondition()
        {
            this.PageSize = 10;
            this.CurrentPage = 1;
            this.Keyword = "";
        }

        private int currentPage;
        private int pageSize;
        private string keyword;

        /// <summary>
        /// 分页索引
        /// </summary>
        public int CurrentPage
        {
            get
            {
                if (currentPage < 1)
                    currentPage = 1;
                return currentPage;
            }
            set { currentPage = value; }
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize
        {
            get
            {
                if (pageSize < 1)
                    pageSize = 10;
                return pageSize;
            }
            set { pageSize = value; }
        }

        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Keyword
        {
            get
            {
                if (string.IsNullOrEmpty(keyword))
                    keyword = "";
                return keyword;
            }
            set { keyword = value; }
        }
    }
}
