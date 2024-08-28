using CommonManager.Helper;
using EasyWechatModels.Other;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.RestSharp
{
    public  class HttpRestClient
    {
        /// <summary>
        /// POST方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName">服务名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="requestBody">传入json</param>
        /// <param name="apiName">api名称，默认epsapi</param>
        /// <returns></returns>
        public static WebApiResponse<T> RestPost<T>(string serviceName, string methodName, string requestBody, string apiName = "epsapi")
        {
            var result = new WebApiResponse<T>();
            result.Status = false;

            var getwayURL = AppSettingHelper.ReadAppSettings("GetWayURL");
            var client = new RestClient(getwayURL + "/" + apiName + "/" + serviceName + "/" + methodName);
            var request = new RestRequest();
            request.Timeout = -1;
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(requestBody);
            try
            {
                RestResponse response = client.Execute(request);
                result = JsonConvert.DeserializeObject<WebApiResponse<T>>(response.Content);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public static string HttpPost(string url, string requestBody)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Timeout = -1;
            request.Method= Method.Post;
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(requestBody);
            RestResponse response = client.Execute(request);
            return response.Content;
        }
        /// <summary>
        /// 传token的post请求 20230428
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestBody"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string requestBody, string token)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Timeout = -1;
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", token);

            request.AddJsonBody(requestBody);
            RestResponse response = client.Execute(request);
            return response.Content;
        }
        public static string HttpGet(string url, string queryData)
        {

            if (!string.IsNullOrEmpty(queryData))
            {
                url += "?" + queryData;
            }

            var client = new RestClient(url);
            var request = new RestRequest();
            request.Timeout = -1;
            request.Method = Method.Get;
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);
            return response.Content;
        }
        /// <summary>
        /// httpget请求带token 20230428
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryData"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string HttpGet(string url, string queryData, string token)
        {

            if (!string.IsNullOrEmpty(queryData))
            {
                url += "?" + queryData;
            }

            var client = new RestClient(url);
            var request = new RestRequest();
            request.Timeout = -1;
            request.Method = Method.Get;
            var body = @"";
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            request.AddHeader("Authorization", token);
            RestResponse response = client.Execute(request);
            return response.Content;
        }


    }
}
