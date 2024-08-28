using AutoMapper;
using BusinessManager.IService.IBasicService;
using CommonManager.Helper;
using CommonManager.RestSharp;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EasyWechat.WebApi.Controllers.Basic
{
    /// <summary>
    /// 微信
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.WeChat))]
    public class WeHomeController : ControllerBase
    {
        private const string Token = "easywechat";
        public IMapper mapper { get; set; }
        public IUserService userService { get; set; }
        /// <summary>
        /// 微信后台提交配置的时候需要校验的接口
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CommonAPI(string signature, string timestamp, string nonce, string echostr)
        {
            try
            {
                string[] strArr = new string[] { Token, timestamp, nonce };
                Array.Sort(strArr);
                string tmpStr = string.Join(string.Empty, strArr);
                tmpStr = SecurityHelper.Sha1Signature(tmpStr);
                if (tmpStr.ToLower() == signature)
                {
                    return Content(echostr);
                }
                return Content("验证失败");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetToken()
        {
            string appid = "wxc828575f1ae4ac08";
            string appSecurity = "1981741e7adff93a5836094b2c753d4b";
            string token=WechatAccessToken.GetAccessToken(appid, appSecurity);
            return Ok(token);
        }
        /// <summary>
        /// 用来接受消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CommonAPI()
        {
            string resultStr = "";

            return Ok(resultStr);
        }
    }
}
