using EasyWechatModels.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Entitys
{
    public class AllTest
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(IsPrimaryKey=true,IsNullable =false,ColumnDescription ="主键ID",Length =40)]
        public string TestId { get; set; }
        [SugarColumn( IsNullable = true,ColumnDescription ="姓名",ColumnDataType = "nvarchar(255)")]
        public string TestName { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "价格", Length =18,DecimalDigits =2)]
        public double TestPrice { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "数量", Length = 8)]
        public int TestNum { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "姓名2", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string TestName2 { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "姓名3",Length =200)]
        public string TestName3 { get; set; }
        [SugarColumn(IsNullable = true, ColumnDescription = "姓名3", Length = 200,IsIgnore =true)]
        public string TestName4 { get; set; }
    }
}
