using EasyWechatModels.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using System.Xml.Linq;

namespace EasyWechatModels.Entitys
{
    /// <summary>
    /// 用户地址表
    /// </summary>
    [SugarTable(tableName: "Buz_UserAddress")]
    public class BuzUserAddress : IEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "用户Id")]
        public string UserId { get; set; }
        /// <summary>
        /// 省ID
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "省ID")]
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "省名称")]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市ID
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "市ID")]
        public int CityId { get; set; }

        /// <su`mmary>
        /// 市名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "市名称")]
        public string CityName { get; set; }

        /// <summary>
        /// 区ID
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "区ID")]
        public int AreaId { get; set; }

        /// <summary>
        /// 区名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "区名称")]
        public string AreaName { get; set; }

        /// <summary>
        /// 镇ID
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "镇ID")]
        public int CalmId { get; set; }

        /// <summary>
        /// 镇名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "镇名称")]
        public string CalmName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "详细地址")]
        public string DetailAddress { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "邮箱")]
        public string UserEmail { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "姓名")]
        public string Realname { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "移动电话")]
        public string Mobilephone { get; set; }

    }
}
