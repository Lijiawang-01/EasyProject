using BusinessManager.IService.IBasicService;
using CommonManager.Helper;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Other;
using Microsoft.AspNetCore.Mvc;

namespace EasyWechatWebApi.Controllers.Buz
{
    [ApiController]
    [Route("api/Buz/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        public IDicService _DicService { get; set; }
        [HttpGet]
        [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.Basic))]
        public string HelloWord()
        {
            return "Hello Word!";
        }
        [HttpGet]
        [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.Buz))]
        public string ReadConfig()
        {
            var _JWTTokenOptions = AppSettingHelper.ReadAppSettings<JWTTokenOptions>("JWTTokenOptions");
            return _JWTTokenOptions.Audience;
        }
        [HttpGet]
        [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.Basic))]
        public ApiResult HelloDic()
        {
            var list = _DicService.GetDicRes("StatusEnum");
            return ResultHelper.Success(list);
        }
    }
}