using AutoMapper;
using BusinessManager.IService.IBasicService;
using CommonManager.Base;
using CommonManager.Helper;
using CommonManager.SqlSugar;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using EasyWechatModels.Other;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Service.BasicService
{
    public class RoleService : BaseService<BaseRole>, IRoleService
    {
        public bool Add(BaseRoleReq req, string userId)
        {
            var info = _mapper.Map<BaseRole>(req);
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }

        public bool Del(string id)
        {
            var info = _db.Queryable<BaseRole>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }
        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Role WHERE Id IN({ids})") > 0;
        }

        public bool Edit(BaseRoleReq req, string userId)
        {
            var role = _db.Queryable<BaseRole>().First(p => p.Id == req.Id);
            _mapper.Map(req, role);
            role.ModifyUserId = userId;
            role.ModifyDate = DateTime.Now;
            return _db.Updateable(role).ExecuteCommand() > 0;
        }

        public BaseRoleRes GetRoleById(string id)
        {
            var info = _db.Queryable<BaseRole>().First(p => p.Id == id);
            return _mapper.Map<BaseRoleRes>(info);
        }

        public PageInfo<BaseRoleRes> GetRoles(BaseRoleReq req)
        {
            var exp = _db.Queryable<BaseRole>().WhereIF(!string.IsNullOrEmpty(req.Name), p => p.Name.Contains(req.Name));
            var res = exp
                .OrderBy(p => p.Order)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();

            var pageInfo = new PageInfo<BaseRoleRes>(req.PageIndex, req.PageSize, exp.Count(), _mapper.Map<List<BaseRoleRes>>(res));
            return pageInfo;
        }
    }
}
