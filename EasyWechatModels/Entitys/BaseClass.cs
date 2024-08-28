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
    /// 分类表
    /// </summary>
    [SugarTable(tableName: "Base_Class")]
    public class BaseClass : IEntity
    {
        /// <summary>
        /// 分类编码
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription ="分类编码")]
        public string ProClassCode { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "分类名称")]
        public string ProClassName { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
