using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.SwaggerExtend
{
    /// <summary>
    /// 跨域注册方法
    /// </summary>
    public static class CorsExt
    {
        /// <summary>
        /// 注册跨域
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsExt(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("allCors", corsBuilder =>
                {
                    corsBuilder.SetIsOriginAllowed(o => true)//signalr跨域
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }
        /// <summary>
        /// 使用跨域
        /// </summary>
        /// <param name="app"></param>
        public static void UseCorsExt(this WebApplication app)
        {
            app.UseCors("allCors");
        }
    }
}
