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
    /// 商品品牌表
    /// </summary>
    [SugarTable(tableName: "Base_ProductBand")]
    public class BaseProductBand : IEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 品牌Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "品牌Id")]
        public string BandId { get; set; }

    }
}
