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
    /// 通知表
    /// </summary>
    [SugarTable(tableName: "Buz_Notice")]
    public class BuzNotice : IEntity
    {
        /// <summary>
        /// 通知名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "通知名称")]
        public string NoticeName { get; set; }
        /// <summary>
        /// 通知类型
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "通知类型")]
        public int NoticeType { get; set; }
        /// <summary>
        /// 是否有有效期
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "是否有有效期")]
        public bool IsHasValidate { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "开始日期")]
        public DateTime NoticeStartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "结束日期")]
        public DateTime NoticeEndDate { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "内容", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string NoticeText { get; set; }
        /// <summary>
        /// 通知状态
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "通知状态")]
        public int NoticeState { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
