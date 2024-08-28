using BusinessManager.IService.IBasicService;
using CommonManager.Base;
using CommonManager.Helper;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Dto;
using EasyWechatModels.Other;
using Microsoft.AspNetCore.Mvc;

namespace EasyWechat.WebApi.Controllers.Basic
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.User))]
    public class UsersController : BaseController
    {
        public IUserService _UserService { get; set; }

        /// <summary>
        /// 待备注
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
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetUsersById(string id)
        {
            var userInfo = _UserService.GetUsersById(id);
            return ResultHelper.Success(userInfo);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetUsersById2()
        {
            //var userInfo = _UserService.GetUsersRoleById(id);
            return ResultHelper.Error("s",500);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetUsersById3()
        {
            //var userInfo = _UserService.GetUsersRoleById(id);
            return ResultHelper.Success("成功");
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
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
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
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
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Del(string id)
        {
            bool isDel = _UserService.Del(id);
            return ResultHelper.Success(isDel);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult BatchDel(string ids)
        {
            bool isDel = _UserService.BatchDel(ids);
            return ResultHelper.Success(isDel);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult SettingRole(string pid, string rids)
        {
            bool isSet = _UserService.SettingRole(pid, rids);
            return ResultHelper.Success(isSet);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
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
