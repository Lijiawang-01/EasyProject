using CommonManager.Base;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using EasyWechatModels.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.IService.IBasicService
{
    public interface IDicService : IBaseService<BaseDictionary>
    {
        /// <summary>
        /// 跟据字典编码获取列表
        /// </summary>
        /// <param name="dicCode"></param>
        /// <returns></returns>
        List<BaseDictionaryItemRes> GetDicRes(string dicCode);
    }
}
