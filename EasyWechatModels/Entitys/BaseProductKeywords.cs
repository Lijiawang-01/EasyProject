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
    [SugarTable(tableName: "Base_ProductKeys")]
    public class BaseProductKeywords : IEntity
    {
        /// <summary>
        /// 关键词
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "关键词")]
        public string ProductKeywords { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 关键词Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "关键词Id")]
        public string KeywordsId { get; set; }

    }
}
