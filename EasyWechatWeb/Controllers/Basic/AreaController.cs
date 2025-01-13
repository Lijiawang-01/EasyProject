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
    /// <summary>
    /// 区域管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.System))]
    public class AreaController : BaseController
    {
        public IAreaService _AreaService { get; set; }

        /// <summary>
        /// 跟据id获取区域信息
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
        /// 获取区域列表
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
        /// 获取区域下拉列表
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
        /// 添加区域
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Add(BaseAreaReq req)
        {
            bool isAdd = _AreaService.Add(req);
            return ResultHelper.Success(isAdd);
        }
        /// <summary>
        /// 修改区域
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult Edit(BaseAreaReq req)
        {
            bool isEdit = _AreaService.Edit(req);
            return ResultHelper.Success(isEdit);
        }
        /// <summary>
        /// 删除区域
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
        /// 批量删除区域
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
