using BusinessManager.IService.IBasicService;
using CommonManager.Base;
using CommonManager.Helper;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Dto;
using EasyWechatModels.Other;
using Microsoft.AspNetCore.Mvc;

namespace EasyWechat.WebApi.Controllers.Basic
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.User))]
    public class UsersController : BaseController
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService _UserService { get; set; }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult GetUsers(BaseUsersReq req)
        {
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            var pageInfo = _UserService.GetUsers(req);
            return ResultHelper.Success(pageInfo);
        }
        /// <summary>
        /// 跟据id获取用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetUsersById(string id)
        {
            var userInfo = _UserService.GetUsersById(id);
            return ResultHelper.Success(userInfo);
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="req">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Add(BaseUsersReq req)
        {
            //获取当前登录人信息 
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            bool isAdd = _UserService.Add(req, userId);
            return ResultHelper.Success(isAdd);
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="req">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Edit(BaseUsersReq req)
        {
            //获取当前登录人信息
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            bool isEdit = _UserService.Edit(req, userId);
            return ResultHelper.Success(isEdit);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Del(string id)
        {
            bool isDel = _UserService.Del(id);
            return ResultHelper.Success(isDel);
        }
        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            bool isDel = _UserService.BatchDel(ids);
            return ResultHelper.Success(isDel);
        }
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="pid">用户Id</param>
        /// <param name="rids">角色id</param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult SettingRole(string pid, string rids)
        {
            bool isSet = _UserService.SettingRole(pid, rids);
            return ResultHelper.Success(isSet);
        }
        /// <summary>
        /// 修改用户的昵称和密码
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult EditNickNameOrPassword(string nickName, string? password)
        {
            //获取当前登录人信息
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            bool isEdit = _UserService.EditNickNameOrPassword(userId, nickName, password);
            return ResultHelper.Success(isEdit);
        }
    }
}
