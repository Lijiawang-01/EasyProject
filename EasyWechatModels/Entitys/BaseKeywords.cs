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
    /// 关键词表
    /// </summary>
    [SugarTable(tableName: "Base_Keywords")]
    public class BaseKeywords : IEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "名称")]
        public string KeywordsName { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
