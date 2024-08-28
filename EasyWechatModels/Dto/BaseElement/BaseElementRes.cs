using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BaseElementRes
    {
        public string ElementName { get; set; }
        public string AppliedRange { get; set; }
        public string AccessType { get; set; }
        public int ActiveStatus { get; set; }
        public string AccessSql { get; set; }
        public string ClassLibName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int ModularId { get; set; }
        public int ReturnDataType { get; set; }
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
