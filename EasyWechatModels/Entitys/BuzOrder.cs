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
    /// 订单主表
    /// </summary>
    [SugarTable(tableName: "Buz_Order")]
    public class BuzOrder : IEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单编号")]
        public string OrderCode { get; set; }
        /// <summary>
        /// 订单名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单名称")]
        public string OrderName { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单日期")]
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单状态")]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单金额")]
        public double OrderAmount { get; set; }
        /// <summary>
        /// 订单进货金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单进货金额")]
        public double OrderOrgAmount { get; set; }
        /// <summary>
        /// 订单付款日期
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单付款日期")]
        public DateTime OrderPayDate { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单备注", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string OrderDesc { get; set; }
        /// <summary>
        /// 订单关闭备注
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "订单关闭备注", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string OrderCloseDesc { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户Id")]
        public string UserId { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户名称")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户电话
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户电话")]
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户地址")]
        public string UserAddress { get; set; }

    }
}
