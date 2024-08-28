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
    public class RoleController : BaseController
    {
        public IRoleService _RoleService { get; set; }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult GetRoles(BaseRoleReq req)
        {
            var pageInfo = _RoleService.GetRoles(req);
            return ResultHelper.Success(pageInfo);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetRole(string id)
        {
            var role = _RoleService.GetRoleById(id);
            return ResultHelper.Success(role);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Add(BaseRoleReq req)
        {
            //获取当前登录人信息
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            bool isAdd = _RoleService.Add(req, userId);
            return ResultHelper.Success(isAdd);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Edit(BaseRoleReq req)
        {
            //获取当前登录人信息
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            bool isEdit = _RoleService.Edit(req, userId);
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
            //获取当前登录人信息 
            bool isDel = _RoleService.Del(id);
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
            //获取当前登录人信息 
            bool isDel = _RoleService.BatchDel(ids);
            return ResultHelper.Success(isDel);
        }
    }
}
