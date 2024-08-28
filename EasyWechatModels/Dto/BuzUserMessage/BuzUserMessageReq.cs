using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
public class BuzUserMessageReq{
public string FromUserId {get;set;}
public string ToUserId {get;set;}
public string FromUserName {get;set;}
public string ToUserName {get;set;}
public string MessageText {get;set;}
public string MessageStatus {get;set;}
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
