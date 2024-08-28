using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BuzUserAccountReq
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 消费类型
        /// </summary>
        public int AccountType { get; set; }
        /// <summary>
        /// 支出
        /// </summary>
        public double PayAmount { get; set; }
        /// <summary>
        /// 收入
        /// </summary>
        public double InComeAmount { get; set; }
        /// <summary>
        /// 当前
        /// </summary>
        public double CurrentAmount { get; set; }
        public string AccountDesc { get; set; }
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Id { get; set; }
    }
}
