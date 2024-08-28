using CommonManager.SwaggerExtend;
using EasyWechatModels.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MySqlX.XDevAPI.Common;
using NetTaste;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    public static class UserInfoHelper
    {
        //声明一个 IServiceCollection 接口类
        public static IServiceCollection? serviceCollection;
        //获取到 HttpContext  对象
        public static HttpContext Current
        {
            get
            {
                object factory = serviceCollection.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));
                HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
        /// <summary>
        /// 当前身份缓存对象
        /// </summary>
        public static BaseUsersRes? CurUserInfo => GetCurUserInfo();

        /// <summary>
        /// 获取当前用户身份缓存对象
        /// </summary>
        /// <returns></returns>
        public static BaseUsersRes GetCurUserInfo()
        {
            var result = new BaseUsersRes();
            var claims = Current.User.Claims.ToList();
            var userId = claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;
            var nickName = claims.Where(x => x.Type == "nickName").FirstOrDefault()?.Value;
            var name = claims.Where(x => x.Type == "name").FirstOrDefault()?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                result.Id = userId;
                result.Name = name;
                result.NickName = nickName;
            }
            return result;
        }
    }
}
