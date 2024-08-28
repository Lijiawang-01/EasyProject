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
    /// 字典表
    /// </summary>
    [SugarTable(tableName: "Base_Dictionary")]
    public class BaseDictionary : IEntity
    {
        /// <summary>
        /// 字典编码
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "字典编码")]
        public string DicCode { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "字典名称")]
        public string DicName { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<BaseDictionaryItem> ItemList { get; set; }

    }
}
