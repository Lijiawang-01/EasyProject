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
    /// 商品分类表
    /// </summary>
    [SugarTable(tableName: "Base_ProductClass")]
    public class BaseProductClass : IEntity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品Id")]
        public string ProductId { get; set; }
        /// <summary>
        /// 分类Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "分类Id")]
        public string ClassId { get; set; }

    }
}
