using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BaseAttachmentRes
    {
        public string AttachmentName { get; set; }
        public string AttachmentGuid { get; set; }
        public string AttachmentType { get; set; }
        public string AttachmentPath { get; set; }
        public double AttachmentSize { get; set; }
        public string AttachmentGroupId { get; set; }
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
