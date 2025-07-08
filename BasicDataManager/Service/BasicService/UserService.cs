using AutoMapper;
using BusinessManager.IService.IBasicService;
using CommonManager.Base;
using CommonManager.Helper;
using CommonManager.SqlSugar;
using CommonManager.Utity;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using EasyWechatModels.Other;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NPOI.POIFS.Crypt.Dsig;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Service.BasicService
{
    public class UserService : BaseService<BaseUsers>, IUserService
    {
        public BaseUsersRes GetUser(string userName, string password)
        {
            var user = _db.Queryable<BaseUsers>().Where(u => u.Name == userName && u.Password == password).First();
            if (user != null)
            {
                return _mapper.Map<BaseUsersRes>(user);
            }
            return new BaseUsersRes();
        }
        public bool Add(BaseUsersReq input, string userId)
        {
            var info = _mapper.Map<BaseUsers>(input);
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.UserType = 1;//0=炒鸡管理员，系统内置的
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }

        public async Task<int> AddUser(BaseUsersReq input, string userId)
        {
            var info = _mapper.Map<BaseUsers>(input);
            info.CreateUserId = userId;
            info.CreateDate = DateTime.Now;
            info.UserType = 1;//0=炒鸡管理员，系统内置的
            info.IsDeleted = 0;
            return await AddAsync(info);
        }
        public bool Edit(BaseUsersReq input, string userId)
        {
            var info = _db.Queryable<BaseUsers>().First(p => p.Id == input.Id);
            _mapper.Map(input, info);
            info.ModifyUserId = userId;
            info.ModifyDate = DateTime.Now;
            return _db.Updateable(info).ExecuteCommand() > 0;
        }

        public bool Del(string id)
        {
            var info = _db.Queryable<BaseUsers>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }

        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Users WHERE Id IN({ids})") > 0;
        }

        public PageInfo<BaseUsersRes> GetUsers(BaseUsersReq req)
        {
            var exp = _db.Queryable<BaseUsers>()
                .LeftJoin<BaseUserRole>((a, b) => a.Id == b.UserId)
                .LeftJoin<BaseRole>((a, b, c) => b.RoleId == c.Id)
                //默认只查询非炒鸡管理员的用户
                //.Where(a => a.UserType == 1)
                .WhereIF(!string.IsNullOrEmpty(req.Name), a => a.Name.Contains(req.Name))
                .WhereIF(!string.IsNullOrEmpty(req.NickName), a => a.NickName.Contains(req.NickName))
                .OrderBy(a => a.CreateDate, OrderByType.Desc)
                .Select((a, b, c) => new BaseUsersRes
                {
                    Id = a.Id,
                    Name = a.Name,
                    NickName = a.NickName,
                    Password = a.Password,
                    UserType = a.UserType,
                    RoleName = c.Name,
                    CreateDate = a.CreateDate.ToString("yyyy-MM-dd"),
                    IsEnable = a.IsEnable,
                    Description = a.Description
                });
            int totalNumber = 0;
            var res = exp.ToPageList(req.PageIndex, req.PageSize, ref totalNumber);
            var pageInfo = new PageInfo<BaseUsersRes>(req.PageIndex, req.PageSize, exp.Count(), res);
            return pageInfo;
        }
        private string GetRolesByUserId(string uid)
        {
            var roleStr = _db.Ado.SqlQuery<string>($@"SELECT  R.Name FROM Role R
                    LEFT JOIN UserRoleRelation UR ON R.Id=UR.RoleId
                    WHERE UR.UserId='{uid}' ");
            if (roleStr.Count() > 0)
            {
                return roleStr[0];
            }
            return "";
        }
        public BaseUsersRes GetUsersById(string id)
        {
            var info = _db.Queryable<BaseUsers>().First(p => p.Id == id);
            return _mapper.Map<BaseUsersRes>(info);
        }
        public bool SettingRole(string pid, string rids)
        {
            //1,2,3,4,5
            List<BaseUserRole> list = new List<BaseUserRole>();
            foreach (string it in rids.Split(','))
            {
                var info = new BaseUserRole() { UserId = pid, RoleId = it.Replace("'", "") };
                list.Add(info);
            }
            //删除之前的角色
            _db.Ado.ExecuteCommand($"DELETE from UserRoleRelation WHERE UserId = '{pid}'");
            return _db.Insertable(list).ExecuteCommand() > 0;
        }
        public bool EditNickNameOrPassword(string userId, string nickName, string password)
        {
            var info = _db.Queryable<BaseUsers>().Where(p => p.Id == userId).First();
            if (info != null)
            {
                if (!string.IsNullOrEmpty(nickName))
                {
                    info.NickName = nickName;
                }
                if (!string.IsNullOrEmpty(password))
                {
                    info.Password = password;
                }
            }
            return _db.Updateable(info).ExecuteCommand() > 0;
        }

    }
}
