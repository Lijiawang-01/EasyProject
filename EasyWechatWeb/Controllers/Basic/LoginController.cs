using AutoMapper;
using BusinessManager.IService.IBasicService;
using CommonManager.Helper;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Dto;
using EasyWechatModels.Other;
using Microsoft.AspNetCore.Mvc;

namespace EasyWechat.WebApi.Controllers.Basic
{
    /// <summary>
    /// 1用户登录
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        public IMapper mapper { get; set; }
        public IUserService userService { get; set; }
        /// <summary>
        /// 跟据用户名和密码获取token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetToken(string name, string password)
        {

            var res = Task.Run(() =>
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                {
                    return ResultHelper.Error("参数不能为空");
                }
                BaseUsersRes user = userService.GetUser(name, password);
                if (string.IsNullOrEmpty(user.Name))
                {
                    return ResultHelper.Error("账号不存在，用户名或密码错误！");
                }
                string token = CustomerJWTHelper.GetToken(user);
                return ResultHelper.Success(token);
            });
            return await res;
        }
    }
}
