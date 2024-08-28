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
    public interface IUserService:IBaseService<BaseUsers>
    {
        /// <summary>
        /// 登录用
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        BaseUsersRes GetUser(string userName, string password);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Add(BaseUsersReq role, string userId);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Edit(BaseUsersReq role, string userId);
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
        /// 获取用户列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        PageInfo<BaseUsersRes> GetUsers(BaseUsersReq req);
        /// <summary>
        /// 根据id获取单个用户
        /// </summary>
        /// <param name = "id" ></ param >
        /// < returns ></ returns >
        BaseUsersRes GetUsersById(string id);
        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="rids"></param>
        /// <returns></returns>
        bool SettingRole(string pid, string rids);
        /// <summary>
        /// 个人中心修改用户昵称或者密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="nickName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool EditNickNameOrPassword(string userId, string nickName, string password);
    }
}
