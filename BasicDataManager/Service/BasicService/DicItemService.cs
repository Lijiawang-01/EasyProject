using AutoMapper;
using BusinessManager.IService.IBasicService;
using CommonManager.Base;
using CommonManager.Helper;
using CommonManager.SqlSugar;
using EasyWechatModels.Entitys;
using EasyWechatModels.Other;
using Microsoft.AspNetCore.Components;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Service.BasicService
{
    public class DicItemService : BaseService<BaseDictionaryItem>, IDicItemService
    {
    }
}
