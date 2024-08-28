using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
public class BuzOrderRes{
public string OrderCode {get;set;}
public string OrderName {get;set;}
public DateTime OrderDate {get;set;}
public int OrderStatus {get;set;}
public double OrderAmount {get;set;}
public double OrderOrgAmount {get;set;}
public DateTime OrderPayDate {get;set;}
public string OrderDesc {get;set;}
public string OrderCloseDesc {get;set;}
public string UserId {get;set;}
public string UserName {get;set;}
public string UserPhone {get;set;}
public string UserAddress {get;set;}
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
