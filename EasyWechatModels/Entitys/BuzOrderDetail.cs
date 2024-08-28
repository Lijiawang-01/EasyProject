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
    /// 订单明细表
    /// </summary>
    [SugarTable(tableName: "Buz_OrderDetail")]
    public class BuzOrderDetail : IEntity
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单Id")]
        public string OrderId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品编码")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品名称")]
        public string ProductName { get; set; }
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
        /// <summary>
        /// 商品最终金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品最终金额")]
        public double FinalAmount { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "优惠金额")]
        public double ReduceAmount { get; set; }
        /// <summary>
        /// 活动Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "活动Id")]
        public string PromotionId { get; set; }

    }
}
