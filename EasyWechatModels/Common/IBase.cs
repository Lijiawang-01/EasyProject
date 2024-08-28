using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Common
{
    public class IBase
    {
        public IBase() {
            Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsNullable = false)]
        public string Id { get; set; }
    }
}
