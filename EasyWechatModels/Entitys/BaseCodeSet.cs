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
    /// 编码设置表
    /// </summary>
    [SugarTable(tableName: "Base_CodeSet")]
    public class BaseCodeSet : IEntity
    {
        /// <summary>
        /// 编码设置名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "编码设置名称")]
        public string CodeSetName { get; set; }
        /// <summary>
        /// 编码设置前缀
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "编码设置前缀")]
        public string CodeSetCode { get; set; }
        /// <summary>
        /// 日期格式
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "日期格式")]
        public int DateFormat { get; set; }
        /// <summary>
        /// 自定义位数
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "自定义位数")]
        public int NumLength { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }
    }
}
