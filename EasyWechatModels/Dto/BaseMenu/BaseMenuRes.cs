using EasyWechatModels.Dto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BaseMenuRes
    {
        public string Name { get; set; }
        public string Index { get; set; }
        public string FilePath { get; set; }
        public string Icon { get; set; }
        public string ParentId { get; set; }
        public int Order { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public string CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        [SugarColumn(IsTreeKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// ×Ó¼¶
        /// </summary>
        public List<BaseMenuRes> Children { get; set; }
    }
}
