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
    /// 消息模板表
    /// </summary>
    [SugarTable(tableName: "Buz_MsgTemplate")]
    public class BuzMsgTemplate : IEntity
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "模板名称")]
        public string TemplateName { get; set; }
        /// <summary>
        /// 模板类型
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "模板类型")]
        public int TemplateType { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "内容", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string TemplateText { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
