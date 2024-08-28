using System;
using System.Linq;
using System.Text;
using EasyWechatModels.Common;
using SqlSugar;

namespace EasyWechatModels.Entitys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Base_Element")]
    public partial class BaseElement : IEntity
    {

        /// <summary>
        /// Desc:元素名称
        /// Default:
        /// Nullable:True
        /// </summary>     
        [SugarColumn(IsNullable = true, ColumnDescription = "元素名称")]
        public string ElementName { get; set; }

        /// <summary>
        /// Desc:应用范围
        /// Default:
        /// Nullable:True
        /// </summary>     
        [SugarColumn(IsNullable = true, ColumnDescription = "应用范围")]
        public string AppliedRange { get; set; }

        /// <summary>
        /// Desc:获取类型
        /// Default:
        /// Nullable:True
        /// </summary>     
        [SugarColumn(IsNullable = true, ColumnDescription = "获取类型")]
        public string AccessType { get; set; }

        /// <summary>
        /// Desc:是否生效
        /// Default:
        /// Nullable:True
        /// </summary>      
        [SugarColumn(IsNullable = true)]
        public int ActiveStatus { get; set; }

        /// <summary>
        /// Desc:获取SQL
        /// Default:
        /// Nullable:True
        /// </summary>     
        [SugarColumn(IsNullable = true, ColumnDescription = "获取SQL")]
        public string AccessSql { get; set; }

        /// <summary>
        /// Desc:类库名称
        /// Default:
        /// Nullable:True
        /// </summary>        
        [SugarColumn(IsNullable = true, ColumnDescription = "类库名称")]
        public string ClassLibName { get; set; }

        /// <summary>
        /// Desc:类名称
        /// Default:
        /// Nullable:True
        /// </summary>       
        [SugarColumn(IsNullable = true, ColumnDescription = "类名称")]
        public string ClassName { get; set; }

        /// <summary>
        /// Desc:方法名称
        /// Default:
        /// Nullable:True
        /// </summary>        
        [SugarColumn(IsNullable = true, ColumnDescription = "方法名称")]
        public string MethodName { get; set; }

        /// <summary>
        /// Desc:所属模块
        /// Default:
        /// Nullable:True
        /// </summary>      
        [SugarColumn(IsNullable = true, ColumnDescription = "所属模块")]
        public int ModularId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [SugarColumn(IsNullable = true, ColumnDescription = "")]
        public int ReturnDataType { get; set; }

    }
}
