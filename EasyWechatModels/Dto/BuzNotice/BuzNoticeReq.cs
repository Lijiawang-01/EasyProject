using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
public class BuzNoticeReq{
public string NoticeName {get;set;}
public int NoticeType {get;set;}
public bool IsHasValidate {get;set;}
public DateTime NoticeStartDate {get;set;}
public DateTime NoticeEndDate {get;set;}
public string NoticeText {get;set;}
public int NoticeState {get;set;}
public bool IsEnable {get;set;}
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
