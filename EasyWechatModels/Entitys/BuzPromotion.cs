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
    /// 促销活动表
    /// </summary>
    [SugarTable(tableName: "Buz_Promotion")]
    public class BuzPromotion : IEntity
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "活动名称")]
        public string PromotionName { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "活动类型")]
        public int PromotionType { get; set; }
        /// <summary>
        /// 活动开始日期
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "活动开始日期")]
        public DateTime ProStartDate { get; set; }
        /// <summary>
        /// 活动结束日期
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "活动结束日期")]
        public DateTime ProEndDate { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "金额")]
        public double ProAmount { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "折扣")]
        public double ProPercent { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
