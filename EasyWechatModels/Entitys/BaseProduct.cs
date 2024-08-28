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
    /// 商品表
    /// </summary>
    [SugarTable(tableName: "Base_Product")]
    public class BaseProduct : IEntity
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        [SugarColumn(IsNullable = true,ColumnDescription = "商品编码")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品名称")]
        public string ProductName { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品描述", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string ProductDesc { get; set; }
        /// <summary>
        /// 商品颜色
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品颜色")]
        public string ProductColor { get; set; }
        /// <summary>
        /// 商品性别
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品性别")]
        public int ProductSex { get; set; }
        /// <summary>
        /// 商品logo文件guid
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品logo")]
        public string ProductLogo { get; set; }
        /// <summary>
        /// 商品视频文件guid
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品视频，文件guid")]
        public string ProductVideo { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品价格")]
        public double ProductPrice { get; set; }
        /// <summary>
        /// 商品成本
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品成本")]
        public double ProductCost { get; set; }
        /// <summary>
        /// 商品积分
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品积分")]
        public int ProductPoint { get; set; }
        /// <summary>
        /// 商品会员折扣
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品会员折扣")]
        public int ProductDiscount { get; set; }
        /// <summary>
        /// 商品尺寸
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品尺寸")]
        public string ProductWeight { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品数量")]
        public int ProductNumber { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "是否推荐")]
        public bool IsActivity { get; set; }
        /// <summary>
        /// 是否新品
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "是否新品")]
        public bool IsNewProduct { get; set; }
        /// <summary>
        /// 商品排序
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "商品排序")]
        public int ProductOrder { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsEnable { get; set; }

    }
}
