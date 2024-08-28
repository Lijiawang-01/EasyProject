using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
public class BuzOrderDetailRes{
public string OrderId {get;set;}
public string ProductId {get;set;}
public string ProductCode {get;set;}
public string ProductName {get;set;}
public double ProductPrice {get;set;}
public double ProductNumber {get;set;}
public double ProductAmount {get;set;}
public double FinalAmount {get;set;}
public double ReduceAmount {get;set;}
public string PromotionId {get;set;}
public string Description {get;set;}
public string CreateUserId {get;set;}
public DateTime CreateDate {get;set;}
public string ModifyUserId {get;set;}
public int IsDeleted {get;set;}
public int PageIndex {get;set;}
public int PageSize {get;set;}
public string Id {get;set;}
}
}
