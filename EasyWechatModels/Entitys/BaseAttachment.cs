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
    /// 文档表
    /// </summary>
    [SugarTable(tableName: "Base_Attachment")]
    public class BaseAttachment : IEntity
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "文件名称")]
        public string AttachmentName { get; set; }
        /// <summary>
        /// 文件存储名称guid
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "文件存储名称guid")]
        public string AttachmentGuid { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "文件类型")]
        public string AttachmentType { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "文件地址")]
        public string AttachmentPath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "文件大小")]
        public double AttachmentSize { get; set; }
        /// <summary>
        /// 文件组id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "文件组id")]
        public string AttachmentGroupId { get; set; }

    }
}
