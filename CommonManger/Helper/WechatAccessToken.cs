using CommonManager.RestSharp;
using EasyWechatModels.Other;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    /// <summary>
    /// 微信AccessToken
    /// </summary>
    public static class WechatAccessToken
    {
        private static string _appId;
        private static string _appSecret;
        private static string _accessToken;
        private static DateTime _expiresIn;

        public static string GetAccessToken(string appId, string appSecret)
        {
            _appId = appId;
            _appSecret = appSecret;
            if (string.IsNullOrEmpty(_accessToken))
            {
                string tokenUrl = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appId}&secret={appSecret}";
                var result = HttpRestClient.HttpGet(tokenUrl, "");
                var tokenInfo = JsonHelper.ToObject<WechatTokenInfo>(result);
                if (!string.IsNullOrEmpty(tokenInfo.errcode))
                    return tokenInfo.errcode;
                var dt = DateTime.Now.AddMilliseconds(tokenInfo.expires_in - 300);
                _expiresIn = dt;
                _accessToken = tokenInfo.access_token;
            }
            return _accessToken;
        }
    }
}
