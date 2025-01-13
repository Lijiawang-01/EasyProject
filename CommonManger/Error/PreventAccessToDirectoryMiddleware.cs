using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Error
{
    /// <summary>
    /// 防止访问特定目录中间件
    /// </summary>
    public class PreventAccessToDirectoryMiddleware
    {
        private readonly RequestDelegate _next;

        public PreventAccessToDirectoryMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value.TrimEnd('/') + '/';
            if (path.StartsWith("/ConfigFile/", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Access to the directory is forbidden.");
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class PreventAccessToDirectoryMiddlewareExtensions
    {
        public static IApplicationBuilder UsePreventAccessToDirectory(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PreventAccessToDirectoryMiddleware>();
        }
    }
}
