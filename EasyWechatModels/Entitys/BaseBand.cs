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
    /// 品牌表
    /// </summary>
    [SugarTable(tableName: "Base_Band")]
    public class BaseBand : IEntity
    {
        /// <summary>
        /// 品牌编码
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "品牌编码")]
        public string ProBandCode { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "品牌名称")]
        public string ProBandName { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
