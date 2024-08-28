using CommonManager.Helper;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Utity
{
    /// <summary>
    /// 0、先执行控制器的构造函数
    /// 1、执行OnActionExecuting
    /// 2、执行Action方法-
    /// 3、执行OnActionExecuted方法
    /// 适合记录日志
    /// </summary>
    public class CustomerActionFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //使用JWT
            string token = context.HttpContext.Request.Headers["Authorization"];
            //去掉前面的Bearer
            if (token != null && token.StartsWith("Bearer"))
                token = token.Substring("Bearer ".Length).Trim();
            //Console.WriteLine("CustomerResourceFilter.OnActionExecuting");
            var controllerName = context.HttpContext.GetRouteValue("controller");
            var actionName = context.HttpContext.GetRouteValue("action");
            var para = context.HttpContext.Request.QueryString.Value;
            string json = CustomerJWTHelper.ValidateJwtToken(token);
            BaseUsersRes userInfo = JsonConvert.DeserializeObject<BaseUsersRes>(json);
            var claimsIdentity = new ClaimsIdentity(new Claim[] {
                        new Claim("id", userInfo.Id.ToString()),
                        new Claim("nickName", userInfo.NickName.ToString()),
                        new Claim("name", userInfo.Name.ToString()),
                        new Claim("userType", userInfo.UserType.ToString())
                    });
            var principal = new ClaimsPrincipal(claimsIdentity);
            context.HttpContext.User = principal;
            //使用cookie
            context.HttpContext.Request.Cookies.TryGetValue("one", out string outOne);
            //LogHelper.Info($"执行控制器{controllerName}-{actionName}.参数{para}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controllerName = context.HttpContext.GetRouteValue("controller");
            var actionName = context.HttpContext.GetRouteValue("action");
            //LogHelper.Info($"执行控制器{controllerName}-{actionName}.结果{context.Result}");
            //Console.WriteLine("CustomerResourceFilter.OnActionExecuted");
            string key = context.HttpContext.Request.Path;
            var l = context.HttpContext.User.Claims.ToList();
            Dictionary<string, object> user = new Dictionary<string, object>();
            l.ForEach(x =>
            {
                user.Add(x.Type, x.Value);
            });
            var toke = new CustomerJWTHelper().Encode(user);
            context.HttpContext.Response.Headers.Add("ucmltoken", toke);

        }
    }
}
