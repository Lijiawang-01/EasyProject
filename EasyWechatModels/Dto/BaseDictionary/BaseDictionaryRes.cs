using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BaseDictionaryRes
    {
        public string DicCode { get; set; }
        public string DicName { get; set; }
        public bool IsEnable { get; set; }
        public string Description { get; set; }
        public string CreateUserId { get; set; }
        public string CreateDate { get; set; }
        public string ModifyUserId { get; set; }
        public int IsDeleted { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BaseDictionaryItemRes> ItemList { get; set; }
    }
}
