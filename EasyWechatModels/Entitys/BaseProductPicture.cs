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
    /// 商品关键词表
    /// </summary>
    [SugarTable(tableName: "Base_ProductPic")]
    public class BaseProductPicture : IEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "关键词")]
        public string ProductKeywords { get; set; }

    }
}
