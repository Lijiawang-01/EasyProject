using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BaseUsersRes
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string PayPassword { get; set; }
        public string UserLogo { get; set; }
        public double UserAmount { get; set; }
        public double UserCostAmount { get; set; }
        public double UserPoints { get; set; }
        public double UserTotalPoints { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public int UserType { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public string CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Id { get; set; }
        public string RoleName { get; set; }
    }
}
