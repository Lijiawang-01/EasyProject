using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    /// <summary>
    /// Session 操作类
    /// 1、GetSession(string name)根据session名获取session对象
    /// 2、SetSession(string name, object val)设置session
    /// </summary>
    public class SessionHelper
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
        /// 根据session名获取session对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSession(string name)
        {
            var obj= Current.Session.GetString(name);
            if(obj == null)
            {
                return "";
            }
            return obj;
        }
        /// <summary>
        /// 设置session,sharejs.com
        /// </summary>
        /// <param name="name">session 名</param>
        /// <param name="val">session 值</param>
        public static void SetSession(string name, string val)
        {
            Current.Session.Remove(name);
            Current.Session.SetString(name, val);
        }
    }
}
