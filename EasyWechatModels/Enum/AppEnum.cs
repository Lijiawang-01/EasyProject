using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Enum
{
    public class AppEnum
    {
        /// <summary>
        /// 审核状态
        /// </summary>
        [Description("审核状态")]
        public enum StatusEnum
        {
            /// <summary>
            /// 草稿
            /// </summary>
            [Description("草稿")]
            Draft = 0,
            /// <summary>
            /// 待处理
            /// </summary>
            [Description("待处理")]
            WaitDispose = 1,
            /// <summary>
            /// 待审核
            /// </summary>
            [Description("待审核")]
            WaitAduit = 2,
            /// <summary>
            /// 审核中
            /// </summary>
            [Description("审核中")]
            Auditing = 3,
            /// <summary>
            /// 审核不通过
            /// </summary>
            [Description("审核不通过")]
            AuditNotPass = 4,
            /// <summary>
            /// 审核通过
            /// </summary>
            [Description("审核通过")]
            AuditPass = 5,
            /// <summary>
            /// 终止
            /// </summary>
            [Description("终止")]
            Stop = 6,
        }
        /// <summary>
        /// 文件上传模式
        /// </summary>
        [Description("文件上传模式")]
        public enum UploadModeEnum
        {
            /// <summary>
            /// 本地
            /// </summary>
            [Description("本地")]
            Loacl = 0,
            /// <summary>
            /// 云端
            /// </summary>
            [Description("云端")]
            Cloud = 1
        }
        /// <summary>
        /// 日期格式
        /// </summary>
        [Description("日期格式")]
        public enum DateFormatEnum
        {
            /// <summary>
            /// 年份2024
            /// </summary>
            [Description("YYYY")]
            Year = 0,
            /// <summary>
            /// 年月202401
            /// </summary>
            [Description("YYYYMM")]
            YearMonth = 1,
            /// <summary>
            /// 年月日20240101
            /// </summary>
            [Description("YYYYMMDD")]
            Date = 2,
            /// <summary>
            /// 年月日时2024010112
            /// </summary>
            [Description("YYYYMMDDHH")]
            DateHour = 3,
            /// <summary>
            /// 年月日时分202401011210
            /// </summary>
            [Description("YYYYMMDDHHmm")]
            DateMinute = 4,
            /// <summary>
            /// 年月日时分秒20240101121011
            /// </summary>
            [Description("YYYYMMDDHHmmss")]
            DateSecond = 5,
        }
        /// <summary>
        /// 性别
        /// </summary>
        [Description("性别")]
        public enum ProductSexEnum
        {
            /// <summary>
            /// 全部
            /// </summary>
            [Description("全部")]
            All = 0,
            /// <summary>
            /// 男
            /// </summary>
            [Description("男")]
            Man = 1,
            /// <summary>
            /// 女
            /// </summary>
            [Description("女")]
            Woman = 2
        }
    }
}
