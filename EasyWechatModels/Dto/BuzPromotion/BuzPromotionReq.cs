using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BuzPromotionReq
    {
        public string PromotionName { get; set; }
        public int PromotionType { get; set; }
        public DateTime ProStartDate { get; set; }
        public DateTime ProEndDate { get; set; }
        public double ProAmount { get; set; }
        public double ProPercent { get; set; }
        public bool IsEnable { get; set; }
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
