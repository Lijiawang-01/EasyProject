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
    /// 采购明细表
    /// </summary>
    [SugarTable(tableName: "Buz_RequestDetail")]
    public class BuzRequestDetail : IEntity
    {
        /// <summary>
        /// 采购Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "采购Id")]
        public string RequestId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品价格")]
        public double ProductPrice { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品数量")]
        public double ProductNumber { get; set; }
        /// <summary>
        /// 商品金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品金额")]
        public double ProductAmount { get; set; }

    }
}
