using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Enum
{
    public class CacheEnum
    {
        public enum ExpireType
        {
            /// <summary>
            /// 绝对过期
            /// 注：即自创建一段时间后就过期
            /// </summary>
            Absolute,

            /// <summary>
            /// 相对过期
            /// 注：即该键未被访问后一段时间后过期，若此键一直被访问则过期时间自动延长
            /// </summary>
            Relative,
        }
        public enum CacheType
        {
            /// <summary>
            /// 使用内存缓存(不支持分布式)
            /// </summary>
            Memory,

            /// <summary>
            /// 使用Redis缓存(支持分布式)
            /// </summary>
            Redis
        }
    }
}
