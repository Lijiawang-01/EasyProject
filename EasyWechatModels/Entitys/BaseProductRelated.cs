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
    /// 商品次数相关表
    /// </summary>
    [SugarTable(tableName: "Base_ProductRelated")]
    public class BaseProductRelated : IEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 商品销售数量
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品销售数量")]
        public int ProductSaleNum { get; set; }
        /// <summary>
        /// 商品点击数量
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品点击数量")]
        public int ProductClickNum { get; set; }
        /// <summary>
        /// 商品收藏数量
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品收藏数量")]
        public int ProductCollectNum { get; set; }

    }
}
