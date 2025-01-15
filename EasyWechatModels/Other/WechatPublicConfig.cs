using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Other
{
    /// <summary>
    /// 微信公众号配置
    /// </summary>
    public class WechatPublicConfig
    {
        public string Token { get; set; }
        public string appid { get; set; }
        public string appSecurity { get; set; }
    }
}
