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
    /// 促销活动商品表
    /// </summary>
    [SugarTable(tableName: "Buz_ProProduct")]
    public class BuzProProduct : IEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 活动Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "活动Id")]
        public string PromotionId { get; set; }

    }
}
