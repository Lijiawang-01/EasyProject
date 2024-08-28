using CommonManager.Base;
using CommonManager.Helper;
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
    public interface IAreaService : IBaseService<EasyWechatModels.Entitys.BaseArea>
    {
        bool Add(BaseAreaReq input);

        bool Edit(BaseAreaReq input);

        bool Del(string id);

        bool BatchDel(string ids);

        PageInfo<BaseAreaRes> GetAreasList(BaseAreaReq req);
        BaseAreaRes GetAreaById(string id);
    }
}
