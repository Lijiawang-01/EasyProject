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
    /// 用户角色关系
    /// </summary>
    [SugarTable(tableName: "Base_UserRole")]
    public class BaseUserRole : IBase
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string UserId { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string RoleId { get; set; }
    }
}
