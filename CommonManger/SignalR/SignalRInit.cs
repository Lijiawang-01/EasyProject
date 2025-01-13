using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.SignalR
{
    /// <summary>
    /// 注册SignalR
    /// </summary>
    public static class SignalRInit
    {
        public static void AddSignalRSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddHostedService<CommonManager.SignalR.ChatHelper>();
            services.AddSignalR();
            services.AddSignalR().AddNewtonsoftJsonProtocol();
        }
    }
}
