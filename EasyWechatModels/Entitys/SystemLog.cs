using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Entitys
{
    [SugarTable(tableName:"Sys_SystemLog")]
    public class SystemLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long LogId { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime LogDate { get; set; }
        /// <summary>
        /// 日志头
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string LogThread { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string LogLevel { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string logLogger { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string LogMessage { get; set; }
    }
}
