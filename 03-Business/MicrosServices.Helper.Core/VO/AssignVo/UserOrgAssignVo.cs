﻿using MicrosServices.Entities.Common;
using MicrosServices.Helper.Core.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosServices.Helper.Core.VO
{
    /// <summary>
    /// 用户机构分配
    /// </summary>
    public class UserOrgAssignVo
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UcUsersSetting UsersSettingInfo { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<CheckOptionValue> optionValues { get; set; }
    }
}
