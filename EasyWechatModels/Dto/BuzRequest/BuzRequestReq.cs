using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
public class BuzRequestReq{
public string RequestCode {get;set;}
public string OrderName {get;set;}
public DateTime RequestDate {get;set;}
public int RequestStatus {get;set;}
public double RequestAmount {get;set;}
public string RequestDesc {get;set;}
public string UserId {get;set;}
public string StockId {get;set;}
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
