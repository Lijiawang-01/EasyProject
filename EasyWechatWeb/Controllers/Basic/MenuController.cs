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
    /// 菜单
    /// </summary>
    [ApiController]
    [Route("api/" + nameof(ApiVersionInfo.Basic) + "/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.Basic))]
    public class MenuController : BaseController
    {
        public IMenuService _MenuService { get; set; }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Add(BaseMenuReq req)
        {
            if (string.IsNullOrEmpty(req.ParentId))
            {
                req.ParentId = Guid.Empty.ToString();
            }
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            //获取当前登录人信息 
            req.Id=Guid.NewGuid().ToString();
            bool isAdd = _MenuService.Add(req, userId);
            return ResultHelper.Success(isAdd);
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Edit(BaseMenuReq req)
        {
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            //获取当前登录人信息
            bool isEdit = _MenuService.Edit(req, userId);
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
            bool isDel = _MenuService.Del(id);
            //获取当前登录人信息 
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
            bool isDel = _MenuService.BatchDel(ids);
            return ResultHelper.Success(isDel);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> GetMenus(BaseMenuReq req)
        {
            var res = Task.Run(() =>
            {
                if (string.IsNullOrEmpty(req.ParentId))
                    req.ParentId = Guid.Empty.ToString();
                var pageInfo = _MenuService.GetMenus(req);
                return ResultHelper.Success(pageInfo);
            });
            return await res;
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult SettingMenu(string rid, string mids)
        {
            bool isSetMenu = _MenuService.SettingMenu(rid, mids);
            return ResultHelper.Success(isSetMenu);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetUserMenus()
        {
            var userId = UserInfoHelper.GetCurUserInfo().Id;
            var list = _MenuService.GetMenusByUserId(userId);
            return ResultHelper.Success(list);
        }
    }
}
