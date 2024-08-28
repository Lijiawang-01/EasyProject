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
    /// 用户消费记录表
    /// </summary>
    [SugarTable(tableName: "Buz_UserAccount")]
    public class BuzUserAccount : IEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户Id")]
        public string UserId { get; set; }
        /// <summary>
        /// 消费类型
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "消费类型")]
        public int AccountType { get; set; }
        /// <summary>
        /// 支出金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "支出金额")]
        public double PayAmount { get; set; }
        /// <summary>
        /// 收入金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "收入金额")]
        public double InComeAmount { get; set; }
        /// <summary>
        /// 当前金额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "当前金额")]
        public double CurrentAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "备注")]
        public string AccountDesc { get; set; }



    }
}
