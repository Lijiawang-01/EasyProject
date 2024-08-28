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
    /// 移动菜单表
    /// </summary>
    [SugarTable(tableName: "Base_MobileMenu")]
    public class BaseMobileMenu : IEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "名称")]
        public string MenuName { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "路由地址")]
        public string Index { get; set; }
        /// <summary>
        /// 项目中的页面路径
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "项目中的页面路径")]
        public string FilePath { get; set; }
        /// <summary>
        /// 未激活的icon
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "未激活的icon")]
        public string UnActiveIcon { get; set; }
        /// <summary>
        /// 激活后的icon
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "激活后的icon")]
        public string ActiveIcon { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "父级")]
        public string ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "排序")]
        public int Order { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<BaseMobileMenu> Child { get; set; }
    }
}
