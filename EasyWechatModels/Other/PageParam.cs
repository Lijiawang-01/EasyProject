﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Other
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PageParam
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Dictionary<string, object>? ParamData { get; set; }
    }
}
