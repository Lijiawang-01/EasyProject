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
    /// 角色表
    /// </summary>
    [SugarTable(tableName: "Base_Role")]
    public class BaseRole : IEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Order { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }
    }
}
