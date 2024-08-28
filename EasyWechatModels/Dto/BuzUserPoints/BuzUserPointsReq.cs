using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
public class BuzUserPointsReq{
public string UserId {get;set;}
public int PointsType {get;set;}
public double PayPoints {get;set;}
public double InComePoints {get;set;}
public double CurrentPoints {get;set;}
public string PointsDesc {get;set;}
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
