using BusinessManager.IService.IBasicService;
using BusinessManager.Service.BasicService;
using CommonManager.Base;
using CommonManager.Helper;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using EasyWechatModels.Other;
using Microsoft.AspNetCore.Mvc;

namespace EasyWechat.WebApi.Controllers.Basic
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.System))]
    public class AreaController : BaseController
    {
        public IAreaService _AreaService { get; set; }

        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult GetAreaById(string id)
        {
            var userInfo = _AreaService.GetById(id);
            return ResultHelper.Success(userInfo);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> GetAreaList(BaseAreaReq req)
        {
            var res = Task.Run(() =>
            {
                if (string.IsNullOrEmpty(req.ParentId))
                    req.ParentId = Guid.Empty.ToString();
                var pageInfo = _AreaService.GetAreasList(req);
                return ResultHelper.Success(pageInfo);
            });
            return await res;
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> GetAreaSelectList(BaseAreaReq req)
        {
            var res = Task.Run(() =>
            {
                if (string.IsNullOrEmpty(req.ParentId))
                    req.ParentId = Guid.Empty.ToString();
                var pageInfo = _AreaService.GetAreaSelectList(req);
                return ResultHelper.Success(pageInfo);
            });
            return await res;
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Add(BaseAreaReq req)
        {
            //获取当前登录人信息 
            bool isAdd = _AreaService.Add(req);
            return ResultHelper.Success(isAdd);
        }
        /// <summary>
        /// 待备注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Edit(BaseAreaReq req)
        {
            //获取当前登录人信息
            bool isEdit = _AreaService.Edit(req);
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
            bool isDel = _AreaService.Del(id);
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
            bool isDel = _AreaService.BatchDel(ids);
            return ResultHelper.Success(isDel);
        }
    }
}
