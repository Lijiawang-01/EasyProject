using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
public class BuzMsgTemplateReq{
public string TemplateName {get;set;}
public int TemplateType {get;set;}
public string TemplateText {get;set;}
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
