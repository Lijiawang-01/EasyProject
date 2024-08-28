using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class BaseProductReq
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string ProductColor { get; set; }
        public int ProductSex { get; set; }
        public string ProductLogo { get; set; }
        public string ProductVideo { get; set; }
        public double ProductPrice { get; set; }
        public double ProductCost { get; set; }
        public int ProductPoint { get; set; }
        public int ProductDiscount { get; set; }
        public string ProductWeight { get; set; }
        public int ProductNumber { get; set; }
        public bool IsActivity { get; set; }
        public bool IsNewProduct { get; set; }
        public int ProductOrder { get; set; }
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
