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
    /// 地址表
    /// </summary>
    [SugarTable(tableName: "Base_Area")]
    public class BaseArea : IEntity
    {
        public BaseArea()
        {
            ParentId = Guid.Empty.ToString();
        }
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "名称")]
        public string AreaName { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "编码")]
        public string AreaCode { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "等级")]
        public int AreaLevel { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "路径")]
        public string AreaPath { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "父级")]
        public string ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "排序")]
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "是否启用")]
        public bool IsEnable { get; set; }

        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<BaseArea> Children { get; set; }
    }
}
