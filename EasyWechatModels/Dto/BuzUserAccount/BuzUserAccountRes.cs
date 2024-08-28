using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BuzUserAccountRes
    {
        public string UserId { get; set; }
        public int AccountType { get; set; }
        public double PayAmount { get; set; }
        public double InComeAmount { get; set; }
        public double CurrentAmount { get; set; }
        public string AccountDesc { get; set; }
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public string CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Id { get; set; }
    }
}
