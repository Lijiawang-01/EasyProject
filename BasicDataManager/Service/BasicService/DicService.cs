using AutoMapper;
using BusinessManager.IService.IBasicService;
using CommonManager.Base;
using CommonManager.Helper;
using CommonManager.SqlSugar;
using EasyWechatModels.Dto;
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
    public class DicService : BaseService<BaseDictionary>, IDicService
    {
        /// <summary>
        /// 跟据字典编码获取列表
        /// </summary>
        /// <param name="dicCode"></param>
        /// <returns></returns>
        public List<BaseDictionaryItemRes> GetDicRes(string dicCode)
        {
            var list = _db.Queryable<BaseDictionary>()
                .LeftJoin<BaseDictionaryItem>((a, b) => a.Id == b.DictionaryId)
                .Where(a => a.DicCode == dicCode)
                .OrderBy((a, b) => b.DispalyOrder)
                .Select((a, b) => new BaseDictionaryItemRes
                {
                    ItemName = b.ItemName,
                    ItemValue = b.ItemValue
                }).ToList();
            return list;
        }
    }
}
