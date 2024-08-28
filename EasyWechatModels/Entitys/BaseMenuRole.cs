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
    /// 菜单角色关系
    /// </summary>
    [SugarTable(tableName: "Base_MenuRole")]
    public class BaseMenuRole : IBase
    {
        /// <summary>
        /// 菜单主键
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "菜单主键")]
        public string MenuId { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "角色主键")]
        public string RoleId { get; set; }
    }
}
