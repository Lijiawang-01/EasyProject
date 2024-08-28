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
    /// 用户
    /// </summary>
    [SugarTable(tableName: "Base_User")]
    public class BaseUsers : IEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户名")]
        public string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "昵称")]
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "密码")]
        public string Password { get; set; }
        /// <summary>
        /// 支付密码
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "支付密码")]
        public string PayPassword { get; set; }
        /// <summary>
        /// 用户logo
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户logo")]
        public string UserLogo { get; set; }
        /// <summary>
        /// 用户余额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户余额")]
        public double UserAmount { get; set; }
        /// <summary>
        /// 用户总消费额
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户总消费额")]
        public double UserCostAmount { get; set; }
        /// <summary>
        /// 用户积分
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户积分")]
        public double UserPoints { get; set; }
        /// <summary>
        /// 用户总积分
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户总积分")]
        public double UserTotalPoints { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "电话")]
        public string UserPhone { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户邮箱")]
        public string UserEmail { get; set; }
        /// <summary>
        /// 用户类型（0=超级管理员，1=普通用户）
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户类型")]
        public int UserType { get; set; }
        /// <summary>
        /// 支付折扣
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "支付折扣",DefaultValue ="1")]
        public double PayPercent { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
