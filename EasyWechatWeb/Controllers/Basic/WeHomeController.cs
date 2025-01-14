using AutoMapper;
using BusinessManager.IService.IBasicService;
using CommonManager.Helper;
using CommonManager.RestSharp;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Other;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using SqlSugar.Extensions;
using System.Text;
using System.Xml;

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
        //private const string Token = "easywechat";
        private WechatPublicConfig wechatPublicConfig = AppSettingHelper.ReadAppSettings<WechatPublicConfig>("WechatPublic");
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
                string[] strArr = new string[] { wechatPublicConfig.Token, timestamp, nonce };
                Array.Sort(strArr);
                string tmpStr = string.Join(string.Empty, strArr);
                tmpStr = SecurityHelper.Sha1Signature(tmpStr);
                LogHelper.Info($"微信 signature：{signature};timestamp:{timestamp};nonce:{nonce};echostr:{echostr}.");
                LogHelper.Info($"微信验证 tmpStr：{tmpStr}.");
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
            string appid = wechatPublicConfig.appid;
            string appSecurity = wechatPublicConfig.appSecurity;
            string token = WechatAccessToken.GetAccessToken(appid, appSecurity);
            return Ok(token);
        }
        /// <summary>
        /// 用来接受消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CommonAPI()
        {
            string resultStr = "";
            var re = Request;
            //IHttpBodyControlFeature httpBodyControlFeature = Request.HttpContext.Features.Get<IHttpBodyControlFeature>();
            //if (httpBodyControlFeature != null)
            //{
            //    httpBodyControlFeature.AllowSynchronousIO = true;
            //}
            try
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    var xmlString = await reader.ReadToEndAsync(); // 使用异步读取
                                                                   //var message = JsonHelper.ToJson<WeChatMessage>(xmlString);

                    //// 处理消息
                    //switch (message.MsgType)
                    //{
                    //    case "event":
                    //        HandleEvent(message);
                    //        break;
                    //        // 其他消息类型的处理
                    //}
                    Console.WriteLine(xmlString);
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xmlString);
                    XmlNode xRoot = xdoc.FirstChild;
                    string openId = xRoot["ToUserName"].InnerText.Trim();
                    string ToUserName = xRoot["FromUserName"].InnerText.Trim();

                    string text = "提示文字";
                    string appid = wechatPublicConfig.appid;
                    string appSecurity = wechatPublicConfig.appSecurity;
                    string token = WechatAccessToken.GetAccessToken(appid, appSecurity);
                    string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + token;
                    //返回文字消息
                    string data = "{\"touser\":\"" + ToUserName + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + text + "\"}}";
                    //返回模板消息
                    string poster = "{\"touser\": \"" + ToUserName + "\",\"template_id\":\"kFjLk6cQhnh4tR-KQ6VvpyTmXu4ZgTPdN0bZJrKvlTQ\",\"url\":\"http://www.sogou.com\",\"data\":{\"name\":{\"value\":\"测试\"},\"age\":{\"value\":\"18\"},\"url2\":{\"value\":\"详细信息\"},\"remark\":{\"value\":\"点击这里查看详情。\",\"color\":\"#0000ff\"} }}";
                    //返回图文消息
                    string poster1 = "{ \"touser\":\"" + ToUserName + "\",\"msgtype\":\"news\",\"news\":{\"articles\": [{\"title\":\"Happy Day\",\"description\":\"Is Really A Happy Day\",\"url\":\"http://www.baidu.com\",\"picurl\":\"PIC_URL\"}]}}";
                    // Request.HttpContext.Response.WriteAsync(TextMessage(ToUserName, openId, sendmsg));//防止提示故障
                    //发送文字消息
                    //string result = HttpRestClient.HttpPost(url, data);
                    //发送模板消息
                    string result1 = HttpRestClient.HttpPost(url, poster1);
                    //发送模板消息
                    //string result2 = HttpRestClient.HttpPost("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token, poster);
                    return Ok("Success");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        #region 返回消息备注
        //{
        //       // 发送文本
        //{
        //    "touser":"OPENID",
        //    "msgtype":"text",
        //    "text":
        //    {
        //        "content":"Hello World"
        //    }
        //}
        //    //发送图片
        //{
        //    "touser":"OPENID",
        //    "msgtype":"image",
        //    "image":
        //    {
        //      "media_id":"MEDIA_ID"
        //    }
        //}
        ////发送语音
        //{
        //    "touser":"OPENID",
        //    "msgtype":"voice",
        //    "voice":
        //    {
        //    "media_id":"MEDIA_ID"
        //    }
        //}
        ////发送视频
        //{
        //    "touser":"OPENID",
        //    "msgtype":"video",
        //    "video":
        //    {
        //    "media_id":"MEDIA_ID",
        //      "thumb_media_id":"THUMB_MEDIA_ID"
        //    }
        //}
        ////发送音乐消息
        //{
        //    "touser":"OPENID",
        //    "msgtype":"music",
        //    "music":
        //    {
        //    "title":"MUSIC_TITLE",
        //      "description":"MUSIC_DESCRIPTION",
        //      "musicurl":"MUSIC_URL",
        //      "hqmusicurl":"HQ_MUSIC_URL",
        //      "thumb_media_id":"THUMB_MEDIA_ID"
        //    }
        //}
        ////发送图文消息（支持1-10条图文展示）
        //{
        //    "touser":"OPENID",
        //    "msgtype":"news",
        //    "news":{
        //    "articles": [
        //         {
        //        "title":"Happy Day",
        //             "description":"Is Really A Happy Day",
        //             "url":"URL",
        //             "picurl":"PIC_URL"
        //         }
        //         ]
        //    }
        //}
        //}
        #endregion
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateMenu()
        {
            string appid = wechatPublicConfig.appid;
            string appSecurity = wechatPublicConfig.appSecurity;
            string token = WechatAccessToken.GetAccessToken(appid, appSecurity);
            var url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + token;
            var res = HttpRestClient.HttpPost(url, @"{""button"":[{""type"":""click"",""name"":""今日歌曲"",""key"":""V1001_TODAY_MUSIC""},{""name"":""菜单"",""sub_button"":[{""type"":""view"",""name"":""搜索"",""url"":""http://www.soso.com/""},{""type"":""click"",""name"":""赞一下我们"",""key"":""V1001_GOOD""}]}]}");
            return Ok(res);
        }
        [HttpGet]
        public IActionResult SendTextToUser()
        {
            string appid = wechatPublicConfig.appid;
            string appSecurity = wechatPublicConfig.appSecurity;
            string token = WechatAccessToken.GetAccessToken(appid, appSecurity);
            var url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + token;
            var res = HttpRestClient.HttpPost(url, @"{""touser"":"""",""msgtype"":""text"",""text"":{""content"":""Hello World""}}");
            return Ok(res);
        }
    }
}
