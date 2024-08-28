using AutoMapper;
using BusinessManager.IService.IBasicService;
using CommonManager.Base;
using CommonManager.Helper;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using EasyWechatModels.Other;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Service.BasicService
{
    public class MenuService : BaseService<BaseMenu>, IMenuService
    {
        public bool Add(BaseMenuReq input, string MenuId)
        {
            var info = _mapper.Map<BaseMenu>(input);
            info.CreateUserId = MenuId;
            info.CreateDate = DateTime.Now;
            info.IsDeleted = 0;
            return _db.Insertable(info).ExecuteCommand() > 0;
        }

        public bool Edit(BaseMenuReq input, string userId)
        {
            var info = _db.Queryable<BaseMenu>().First(p => p.Id == input.Id);
            _mapper.Map(input, info);
            info.ModifyUserId = userId;
            info.ModifyDate = DateTime.Now;
            return _db.Updateable(info).ExecuteCommand() > 0;
        }

        public bool Del(string id)
        {
            var info = _db.Queryable<BaseMenu>().First(p => p.Id == id);
            return _db.Deleteable(info).ExecuteCommand() > 0;
        }

        public bool BatchDel(string ids)
        {
            return _db.Ado.ExecuteCommand($"DELETE Base_Menu WHERE Id IN({ids})") > 0;
        }

        public PageInfo<BaseMenuRes> GetMenus(BaseMenuReq req)
        {
            var exp = _db.Queryable<BaseMenu>()
               .WhereIF(!string.IsNullOrEmpty(req.Name), u => u.Name.Contains(req.Name))
               .WhereIF(!string.IsNullOrEmpty(req.Index), u => u.Index.Contains(req.Index))
               .OrderBy((u) => u.Order)
               .Select((u) => new BaseMenuRes
               {
                   Id = u.Id,
                   Name = u.Name,
                   Index = u.Index,
                   FilePath = u.FilePath,
                   ParentId = u.ParentId,
                   Order = u.Order,
                   IsEnable = u.IsEnable,
                   Description = u.Description,
                   CreateDate = u.CreateDate.ToString("yyyy-MM-dd")
               }).ToTree(it => it.Children, it => it.ParentId, Guid.Empty);

            var res = exp
                .OrderBy(p => p.Order)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToList();

            var pageInfo = new PageInfo<BaseMenuRes>(req.PageIndex, req.PageSize, exp.Count(), res);
            return pageInfo;
        }
        public BaseMenuRes GetMenuById(string id)
        {
            var info = _db.Queryable<BaseMenu>().First(p => p.Id == id);
            return _mapper.Map<BaseMenuRes>(info);
        }

        public bool SettingMenu(string rid, string mids)
        {
            List<BaseMenuRole> list = new List<BaseMenuRole>();
            foreach (string it in mids.Split(','))
            {
                var info = new BaseMenuRole() { RoleId = rid, MenuId = it.Replace("'", "") };
                list.Add(info);
            }
            //删除之前的角色
            _db.Ado.ExecuteCommand($"DELETE from Base_MenuRole WHERE RoleId = '{rid}'");
            return _db.Insertable(list).ExecuteCommand() > 0;
        }
        #region 查询当前角色所拥有的菜单
        public List<BaseMenuRes> GetMenusByUserId(string userId)
        {
            List<BaseMenu> res = new List<BaseMenu>();
            //查询当前角色所拥有的菜单
            Expression<Func<BaseMenu, BaseMenuRole, bool>> func = (m, mr) => m.Id == mr.MenuId;
            var currlist = _db.Queryable<BaseMenu>().ToList();
            if (_db.Queryable<BaseUsers>().Where(p => p.Id == userId).First().UserType > 0)
            {
                //如果是普通用户，则根据角色获取菜单
                var idarr = _db.Queryable<BaseUserRole>().Where(P => P.UserId == userId).Select(s => s.RoleId).ToList();
                func = (m, mr) => m.Id == mr.MenuId && idarr.Contains(mr.RoleId);
                currlist = _db.Queryable<BaseMenu>().InnerJoin(func).ToList();
            }
            var all = _db.Queryable<BaseMenu>().Where(p => p.IsEnable).ToList();
            if (currlist != null && currlist.Count > 0)
            {
                currlist.ForEach(item =>
                {
                    //找到这些菜单的父级菜单
                    GetParent(all, item, res);
                });
                //将父级菜单和当前的菜单整合在一起组成完整的菜单
                List<BaseMenuRes> list = _mapper.Map<List<BaseMenuRes>>(currlist.Concat(res).Distinct().ToList());
                //再通过递归的方式讲list转tree
                return GetResult(list);
            }
            return new List<BaseMenuRes>();
        }
        /// <summary>
        /// 递归获取所有父级
        /// </summary>
        /// <param name="all"></param>
        /// <param name="curr"></param>
        /// <param name="res"></param>
        private static void GetParent(List<BaseMenu> all, BaseMenu curr, List<BaseMenu> res)
        {
            var pInfo = all.FirstOrDefault(p => p.Id == curr.ParentId);
            if (pInfo != null)
            {
                res.Add(pInfo);
                GetParent(all, pInfo, res);
            }
        }
        private static List<BaseMenuRes> GetResult(List<BaseMenuRes> list)
        {
            //从一级菜单开始往下找子级菜单
            List<BaseMenuRes> newlist = list.Where(p => p.ParentId == JsonHelper.ToZeroGuid()).ToList();
            //通过递归获取子级
            GetChildren(list, newlist);
            return newlist.Distinct(new DisComparerHelper<BaseMenuRes>("Name")).ToList();
        }
        private static void GetChildren(List<BaseMenuRes> all, List<BaseMenuRes> res)
        {
            if (res != null && res.Count > 0)
            {
                res.ForEach(item =>
                {
                    var child = all.Where(p => p.ParentId == item.Id).Distinct(new DisComparerHelper<BaseMenuRes>("Name")).OrderBy(p => p.Order).ToList();
                    if (child != null && child.Count > 0)
                    {
                        item.Children = child;
                        GetChildren(all, child);
                    }
                });
            }
        }

        #endregion
    }
}
