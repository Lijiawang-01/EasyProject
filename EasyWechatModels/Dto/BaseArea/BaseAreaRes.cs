using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BaseAreaRes
    {
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
        public string ParentId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        [SugarColumn(IsTreeKey = true)]
        public string Id { get; set; }

        public List<BaseAreaRes> Children { get; set; }
    }
}
