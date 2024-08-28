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
    public static class WechatAccessToken
    {
        private static string _appId;
        private static string _appSecret;
        private static string _accessToken;
        private static DateTime _expiresIn;

        public static string GetAccessToken(string appId, string appSecret)
        {
            if (string.IsNullOrEmpty(_accessToken) && _expiresIn > DateTime.Now)
            {
                string tokenUrl = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={_appId}&secret={_appSecret}";
                var result = HttpRestClient.HttpGet(tokenUrl, "");
                var tokenInfo = JsonHelper.ToObject<WechatTokenInfo>(result);
                if (tokenInfo.errcode != "")
                    return tokenInfo.errcode;
                var dt = DateTime.Now.AddMonths(tokenInfo.expires_in - 300);
                _expiresIn = dt;
                _accessToken = tokenInfo.access_token;
            }
            return _accessToken;
        }
    }
}
