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
    /// PC端菜单表
    /// </summary>
    [SugarTable(tableName: "Base_Menu")]
    public class BaseMenu : IEntity
    {
        public BaseMenu()
        {
            ParentId = Guid.Empty.ToString();
        }
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "名称")]
        public string Name { get; set; }
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
        /// 图标icon
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "图标icon")]
        public string Icon { get; set; }
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
        public List<BaseMenu> Child { get; set; }
    }
}
