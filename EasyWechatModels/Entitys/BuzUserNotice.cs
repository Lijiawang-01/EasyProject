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
    /// 用户消息表
    /// </summary>
    [SugarTable(tableName: "Buz_UserNotice")]
    public class BuzUserNotice : IEntity
    {
        /// <summary>
        /// 通知Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "通知Id")]
        public string NoticeId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户Id")]
        public string UserId { get; set; }

    }
}
