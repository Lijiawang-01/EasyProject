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
    /// 库存表
    /// </summary>
    [SugarTable(tableName: "Base_Stock")]
    public class BaseStock : IEntity
    {
        /// <summary>
        /// 库存地编码
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "库存地编码")]
        public string StockCode { get; set; }
        /// <summary>
        /// 库存地名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "库存地名称")]
        public string StockName { get; set; }
        /// <summary>
        /// 库存地地址
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "库存地地址")]
        public string StockAddress { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
