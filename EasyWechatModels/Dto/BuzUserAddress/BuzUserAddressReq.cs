using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
public class BuzUserAddressReq{
public string UserId {get;set;}
public int ProvinceId {get;set;}
public string ProvinceName {get;set;}
public int CityId {get;set;}
public string CityName {get;set;}
public int AreaId {get;set;}
public string AreaName {get;set;}
public int CalmId {get;set;}
public string CalmName {get;set;}
public string DetailAddress {get;set;}
public string UserEmail {get;set;}
public string Realname {get;set;}
public string Mobilephone {get;set;}
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
