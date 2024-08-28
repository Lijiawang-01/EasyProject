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
    /// 采购主表
    /// </summary>
    [SugarTable(tableName: "Buz_Request")]
    public class BuzRequest : IEntity
    {
        /// <summary>
        /// 采购编号
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "采购编号")]
        public string RequestCode { get; set; }
        /// <summary>
        /// 采购名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "采购名称")]
        public string OrderName { get; set; }
        /// <summary>
        /// 采购日期
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "采购日期")]
        public DateTime RequestDate { get; set; }
        /// <summary>
        /// 采购状态
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "采购状态")]
        public int RequestStatus { get; set; }
        /// <summary>
        /// 采购金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "采购金额")]
        public double RequestAmount { get; set; }
        /// <summary>
        /// 采购备注
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "采购备注", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string RequestDesc { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户Id")]
        public string UserId { get; set; }
        /// <summary>
        /// 库存Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "库存Id")]
        public string StockId { get; set; }

    }
}
