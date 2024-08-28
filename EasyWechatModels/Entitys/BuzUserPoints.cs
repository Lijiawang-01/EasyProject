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
    /// 用户积分记录表
    /// </summary>
    [SugarTable(tableName: "Buz_UserPoints")]
    public class BuzUserPoints : IEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户Id")]
        public string UserId { get; set; }
        /// <summary>
        /// 积分类型
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "积分类型")]
        public int PointsType { get; set; }
        /// <summary>
        /// 支出积分
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "支出积分")]
        public double PayPoints { get; set; }
        /// <summary>
        /// 收入积分
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "收入积分")]
        public double InComePoints { get; set; }
        /// <summary>
        /// 当前积分
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "当前积分")]
        public double CurrentPoints { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "备注")]
        public string PointsDesc { get; set; }



    }
}
