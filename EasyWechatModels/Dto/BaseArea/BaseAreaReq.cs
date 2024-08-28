using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BaseAreaReq
    {
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
        /// <summary>
        /// µÈ¼¶
        /// </summary>
        public int AreaLevel { get; set; }
        /// <summary>
        /// Â·¾¶
        /// </summary>
        public string AreaPath { get; set; }
        public string ParentId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Id { get; set; }
        public string NodeId { get; set; }
        public string ParentNodeId { get; set; }
    }
}
