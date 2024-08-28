using EasyWechatModels.Dto;
using EasyWechatModels.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.IService.IBasicService
{
    /// <summary>
    /// 菜单模块接口
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Add(BaseMenuReq role, string userId);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Edit(BaseMenuReq role, string userId);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Del(string id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDel(string ids);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        PageInfo<BaseMenuRes> GetMenus(BaseMenuReq req);
        /// <summary>
        /// 设置菜单
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="mids"></param>
        /// <returns></returns>
        bool SettingMenu(string rid, string mids);
        /// <summary>
        /// 获取当前登录用户所在角色的菜单列表（树列表）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<BaseMenuRes> GetMenusByUserId(string userId);
    }
}
