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
    /// 用户聊天记录表
    /// </summary>
    [SugarTable(tableName: "Buz_UserMessage")]
    public class BuzUserMessage : IEntity
    {
        /// <summary>
        /// 发送用户Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "发送用户Id")]
        public string FromUserId { get; set; }
        /// <summary>
        /// 接收用户Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "接收用户Id")]
        public string ToUserId { get; set; }

        /// <summary>
        /// 发送用户
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "发送用户")]
        public string FromUserName { get; set; }
        /// <summary>
        /// 接收用户
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "接收用户")]
        public string ToUserName { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "信息")]
        public string MessageText { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "状态")]
        public string MessageStatus { get; set; }
    }
}
