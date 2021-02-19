﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.Form
{
    /// <summary>
    /// 用户机构新增
    /// </summary>
    public class UserOrgsForm
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string inputUser { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userNo { get; set; }
        /// <summary>
        /// 机构编号数组
        /// </summary>
        public long[] orgNos { get; set; }
    }
}
