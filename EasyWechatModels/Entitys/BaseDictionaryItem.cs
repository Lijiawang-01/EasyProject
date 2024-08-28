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
    /// 字典项表
    /// </summary>
    [SugarTable(tableName: "Base_DictionaryItem")]
    public class BaseDictionaryItem : IEntity
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "字典Id")]
        public string DictionaryId { get; set; }
        /// <summary>
        /// 字典项名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "字典项名称")]
        public string ItemName { get; set; }
        /// <summary>
        /// 字典项值
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "字典项值")]
        public int ItemValue { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "排序")]
        public int DispalyOrder { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
