using BusinessManager.IService.IBasicService;
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

namespace BusinessManager.Service.BasicService
{
    public class AreaService : BaseService<BaseArea>, IAreaService
    {
        public bool Add(BaseAreaReq input)
        {
            var info = _mapper.Map<BaseArea>(input);
            info.CreateUserId = UserInfoHelper.CurUserInfo.Id;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }

        public bool Edit(BaseAreaReq input)
        {
            var info = _mapper.Map<BaseArea>(input);
            info.ModifyUserId = UserInfoHelper.CurUserInfo.Id;
            info.ModifyDate = DateTime.Now;
            return _db.Updateable(info).ExecuteCommand() > 0;
        }

        public bool Del(string id)
        {
            var info = _db.Queryable<BaseArea>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }

        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Base_Area WHERE Id IN({ids})") > 0;
        }

        public PageInfo<BaseAreaRes> GetAreasList(BaseAreaReq req)
        {
            var exp = _db.Queryable<BaseArea>()
               .WhereIF(!string.IsNullOrEmpty(req.AreaName), u => u.AreaName.Contains(req.AreaName))
               .WhereIF(!string.IsNullOrEmpty(req.AreaCode), u => u.AreaCode.Contains(req.AreaCode))
               .OrderBy((u) => u.DisplayOrder)
               .Select((u) => new BaseAreaRes
               {
                   Id = u.Id
               ,
                   AreaName = u.AreaName
               ,
                   AreaCode = u.AreaCode
               ,
                   ParentId = u.ParentId
               ,
                   DisplayOrder = u.DisplayOrder
               ,
                   IsEnable = u.IsEnable
               ,
                   Description = u.Description
               ,
                   CreateDate = u.CreateDate
               });

            var res = exp
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();

            var pageInfo = new PageInfo<BaseAreaRes>(req.PageIndex, req.PageSize, exp.Count(), res);
            return pageInfo;
        }
        public List<SelectResult> GetAreaSelectList(BaseAreaReq req)
        {
            var exp = _db.Queryable<BaseArea>()
              .WhereIF(!string.IsNullOrEmpty(req.AreaName), u => u.AreaName.Contains(req.AreaName))
              .WhereIF(!string.IsNullOrEmpty(req.AreaCode), u => u.AreaCode.Contains(req.AreaCode))
              .OrderBy((u) => u.DisplayOrder)
              .Select((u) => new SelectResult
              {
                  Id = u.Id
              ,
                  Name = u.AreaName
              });
            return exp.ToList();
        }
        public BaseAreaRes GetAreaById(string id)
        {
            var info = _db.Queryable<BaseArea>().First(p => p.Id == id);
            return _mapper.Map<BaseAreaRes>(info);
        }
    }
}
