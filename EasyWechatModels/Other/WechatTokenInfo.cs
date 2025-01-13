using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Other
{
    /// <summary>
    /// 微信token信息
    /// </summary>
    public class WechatTokenInfo
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}
