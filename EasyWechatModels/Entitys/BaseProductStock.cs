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
    /// 商品库存表
    /// </summary>
    [SugarTable(tableName: "Base_ProductStock")]
    public class BaseProductStock : IEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 库存Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "库存Id")]
        public string StockId { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品数量")]
        public int ProductNumber { get; set; }

    }
}
