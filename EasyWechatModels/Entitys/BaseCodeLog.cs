using EasyWechatModels.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Entitys
{
    /// <summary>
    /// 编码日志表
    /// </summary>
    [SugarTable(tableName: "Base_CodeLog")]
    public class BaseCodeLog : IEntity
    {
        /// <summary>
        /// 编码设置Id
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "编码设置Id")]
        public string CodeSetId { get; set; }
        /// <summary>
        /// 编码设置名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "编码设置名称")]
        public string CodeSetName { get; set; }
        /// <summary>
        /// 当前编码
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "当前编码")]
        public string CurrentCode { get; set; }

    }
}
