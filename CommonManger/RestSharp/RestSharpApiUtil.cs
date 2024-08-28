using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Method = RestSharp.Method;

namespace CommonManager.RestSharp
{
    public class RestSharpApiUtil
    {
        #region 暴露执行方法
        /// <summary>
        /// 组装Client，Request，并执行Http请求
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="baseUrl">基地址</param>
        /// <param name="relativeUrl">相对地址</param>
        /// <param name="method">请求类型</param>
        /// <param name="lstParam">Get/Put/Delete/Post等参数</param>
        /// <param name="obj">post请求体</param>
        /// <returns></returns>
        public static ResponseMessage<T> RestAction<T>(string baseUrl, string relativeUrl, Method method = Method.Get, List<RestParam> lstParam = null)
        {
            var client = new RestClient(baseUrl);
            return RestMethod<T>(client, InstallRequest(relativeUrl, method, lstParam));
        }

        #endregion

        #region 底层调用，并不暴露方法
        /// <summary>
        /// Http请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        static ResponseMessage<T> RestMethod<T>(RestClient client, RestRequest request)
        {
            RestResponse restResponse = (RestResponse)client.Execute(request);
            try
            {
                return restResponse == null ? new ResponseMessage<T>() :
                    string.IsNullOrWhiteSpace(restResponse.Content) ? new ResponseMessage<T>() :
                    JsonConvert.DeserializeObject<ResponseMessage<T>>(restResponse.Content);
            }
            catch (Exception ex)
            {
                return new ResponseMessage<T>() { success = false };
            }
        }


        /// <summary>
        /// 组装Request
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <param name="method"></param>
        /// <param name="lstParam"></param>
        /// <returns></returns>
        static RestRequest InstallRequest(string relativeUrl, Method method = Method.Get, List<RestParam> lstParam = null)
        {
            var request = new RestRequest(relativeUrl, method);
            if (lstParam != null)
            {
                foreach (RestParam p in lstParam)
                {
                    switch (p.ParamType)
                    {
                        case EmParType.UrlSegment:
                            request.AddUrlSegment(p.Key, p.Value);
                            break;
                        case EmParType.Param:
                            request.AddParameter(p.Key, p.Value);
                            break;
                        case EmParType.Body:
                            request.AddJsonBody(p.Value);
                            break;
                        default:
                            break;
                    }
                }
            }
            return request;
        }
        #endregion

    }
}
