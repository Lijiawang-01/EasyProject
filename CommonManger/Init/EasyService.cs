using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using CommonManager.Cache;
using CommonManager.Error;
using CommonManager.Helper;
using CommonManager.SignalR;
using CommonManager.SqlSugar;
using CommonManager.SwaggerExtend;
using CommonManager.Utity;
using Consul;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace CommonManager.Init
{
    public static class EasyService
    {
        public static void InitService(this WebApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            #region log4
            {
                //引入log4net
                //Microsoft.Extensions.Logging.Log4Net.AspNet
                //builder.Logging.AddLog4Net();
                builder.Services.AddLogging(cfg =>
                {
                    //cfg.AddLog4Net();
                    //默认的配置文件路径是在根目录，且文件名为log4net.config
                    //如果文件路径或名称有变化，需要重新设置其路径或名称
                    //比如在项目根目录下创建一个名为cfg的文件夹，将log4net.config文件移入其中，并改名为log.config
                    //则需要使用下面的代码来进行配置
                    cfg.AddLog4Net(new Log4NetProviderOptions()
                    {
                        Log4NetConfigFileName = "ConfigFile/log4net.config",
                        Watch = true
                    });
                });
            }
            #endregion
            #region 动态读取配置文件
            //构建配置对象，读取appsettings.json文件，并将配置封装到AppSettingHelper中，以单例模式注册到服务容器，方便全局获取配置
            IConfiguration configuration = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.json", false, true)
                          .Build();
            builder.Services.AddSingleton(new AppSettingHelper(configuration));
            #endregion
            // Add services to the container.
            #region 注册userInfo中的HttpContext
            ////注册IHttpContextAccessor（用于访问当前请求的HttpContext）为单例，方便在应用中获取用户上下文信息（如请求头、Cookie 等
            //builder.Services.AddHttpContextAccessor();
            //注册IHttpContextAccessor（用于访问当前请求的HttpContext）为单例，方便在应用中获取用户上下文信息（如请求头、Cookie 等）。
            //将服务集合赋值给UserInfoHelper，可能用于后续用户信息相关的服务注册。
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion
            #region swagger&跨域
            //注册swagger
            builder.Services.AddSwaggerExt();
            //注册跨域
            builder.Services.AddCorsExt();
            #endregion
            //builder.Services.InitService();
            #region 禁用数据验证
            //禁用ASP.NET Core 默认的模型验证过滤器，允许使用自定义的模型验证逻辑（需在 API 接口中手动添加验证）
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // 使用自定义模型验证【Api接口需要添加才能生效】
            });
            #endregion
            #region 使用Session 
            //Session的服务端存储需要缓存，所以需要引入.Net core的缓存DistributedMemoryCache；
            //AddDistributedMemoryCache()：注册分布式内存缓存（Session 依赖缓存存储）。
            //配置 Session 参数：指定 Cookie 域名、名称、过期时间（2000 秒），
            //并设置 Cookie 仅 HTTP 访问（防止 JS 读取，增强安全性）
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Domain = "localhost";
                options.Cookie.Name = "EasyWechat.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(2000);//设置session的过期时间
                options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值 
            });
            //SessionHelper.serviceCollection = builder.Services;
            #endregion
            #region 缓存
            if (configuration["IsUseRedis"].ObjToBool())
            {
                //使用redis缓存
                builder.Services.AddSingleton<IRedisCache, RedisCache>();
            }
            else
            {
                //使用内存缓存
                builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
            }
            #endregion
            #region mapper
            #region 方法一 使用Helper
            //注册mapper
            //初始化 AutoMapper 配置，通过AutoMapperConfig类定义对象映射规则，并将配置注册为单例，方便全局使用对象映射（如 DTO 与实体类的转换）
            var config = new MapperConfiguration(e => e.AddProfile(new AutoMapperConfig()));
            builder.Services.AddSingleton(config);
            //注册mapper映射
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            #endregion
            #endregion
            #region mvc
            //注册 MVC 控制器服务，并配置 JSON 序列化规则：
            //自定义日期时间转换器（DatetimeJsonConverter）处理日期格式。
            //属性名采用驼峰命名法（如userName）。
            //支持所有 Unicode 编码（解决中文乱码）。
            //允许 JSON 尾逗号，反序列化时严格区分属性名大小写。
            builder.Services.AddControllers()
                       .AddControllersAsServices()
                       .AddJsonOptions(options =>
                       {
                           //格式化日期时间格式，需要自己创建指定的转换类DatetimeJsonConverter
                           options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
                           //数据格式首字母小写
                           options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                           //JsonNamingPolicy.CamelCase首字母小写（默认）,null则为不改变大小写
                           //取消Unicode编码；防止中文乱码
                           options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                           //忽略空值
                           //options.JsonSerializerOptions.IgnoreNullValues = true;
                           //允许额外符号
                           options.JsonSerializerOptions.AllowTrailingCommas = true;
                           //反序列化过程中属性名称是否使用不区分大小写的比较
                           options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;

                       });
            #endregion
            #region sqlsugar
            builder.Services.AddSqlsugarSetup();
            #endregion
            #region autofac+Castle
            {
                // autofac 替换内置的IOC
                //使用 Autofac 替换默认的依赖注入容器
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
                //构建容器
                builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    //不用了
                    //containerBuilder.RegisterType<SqlSugarHelper>().SingleInstance(); 
                    containerBuilder.RegisterType<CustomerInterceptor>();
                    //containerBuilder.RegisterModule(new AutofacModuleRegister()).
                    var basePath = AppContext.BaseDirectory;
                    #region 注入
                    string path = basePath;
                    System.IO.DirectoryInfo diObj = new System.IO.DirectoryInfo(path);
                    //方法
                    System.IO.FileInfo[] fiChilds = diObj.GetFiles().Where(x => x.Name.EndsWith("Manager.dll")).ToArray();
                    foreach (var fiObj in fiChilds)
                    {
                        var assemblyService = Assembly.LoadFrom(fiObj.FullName);
                        containerBuilder.RegisterAssemblyTypes(assemblyService)
                            .Where(t =>
                            {
                                bool flag = t.Name.EndsWith("Service");
                                return flag;
                            })
                            /*
                             * RegisterAssemblyTypes(程序集数组)，程序集必须是public的
                             * AsImplementedInterfaces()：表示注册的类型，以接口的方式注册
                             * PropertiesAutowired()：支持属性注入
                             * Where：满足条件类型注册
                             * 每次获取都是全新的实例，关键词InstancePerDependency，默认的生命周期 瞬时生命周期
                             * EnableInterfaceInterceptors + 特性标记在抽象上，所有实现类都支持AOP
                             * EnableInterfaceInterceptors + 特性标记到实现类上，标记的类就支持AOP
                             * 使用 ContainerBuilder 的 InterceptedBy() 方法在注册对象的同时添加拦截器
                             */
                            .AsImplementedInterfaces()
                            .InstancePerDependency()
                            .PropertiesAutowired()
                            //.InterceptedBy(interceptorServiceTypes.ToArray())
                            .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
                            {
                                Selector = new CustomeInterceptorSelector()
                            });
                    }
                    //控制器
                    System.IO.FileInfo[] fiChilds2 = diObj.GetFiles().Where(x => x.Name.EndsWith("WebApi.dll")).ToArray();
                    foreach (var fiObj in fiChilds2)
                    {
                        //"Eps.Bas.BigData.Ctrl.dll"
                        //Console.WriteLine("debugFor:"+ fiObj.Name);
                        var assemblysCtrl = Assembly.LoadFrom(fiObj.FullName);
                        containerBuilder.RegisterAssemblyTypes(assemblysCtrl)
                       //.Where(t =>
                       //{
                       //    bool flag = t.Name.EndsWith("Controller");
                       //    if (flag)
                       //    {
                       //        //Console.WriteLine("注入ctrl对象：" + t.FullName);
                       //    }
                       //    return flag;
                       //})
                       .PropertiesAutowired().AsSelf().InstancePerDependency();
                    }
                    #endregion
                    //容器构建完成后，通过AutofacInit初始化容器，方便全局获取服务
                    // 获取到 Autofac 的容器,静态调用方法
                    //controller中直接使用var uservice = CommonManager.Utity.AutofacInit.Resolve<IUserService>();
                    containerBuilder.RegisterBuildCallback(container =>
                    {
                        var _container = (IContainer)container;
                        AutofacInit.InitContainer(_container);
                    });
                });

            }
            #endregion
            #region SignalR
            //signalR支持
            //注册 SignalR 服务，用于实现实时通信（如 WebSocket）
            builder.Services.AddSignalRSetup();
            #endregion
            //核心原理是整合服务注册、主机配置和中间件管道，创建一个可运行的应用实例
            //Build() 方法将 WebApplicationBuilder 中配置的所有服务、主机设置和中间件整合到一起，最终创建一个可运行的 WebApplication 实例
            //构建应用实例，开始配置请求处理管道
            var app = builder.Build();
            #region 使用swagger
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    //使用swagger
            //    app.UseSwaggerExt();
            //}
            if (configuration["IsUseSwagger"].ObjToBool())
            {
                app.UseSwaggerExt();
            }
            #endregion
            #region 使用跨域
            //跨域启用跨域中间件，使前端请求能跨域访问 API
            app.UseCorsExt();
            #endregion
            //app.UseAuthorization();
            #region 使用session
            app.UseSession();
            #endregion
            UserInfoHelper.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
            SessionHelper.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
            //映射控制器路由，使 MVC 控制器能处理 HTTP 请求
            app.MapControllers();
            #region 错误中间件
            //注册全局异常处理中间件，捕获并处理应用中的异常，统一返回错误信息
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            #endregion
            //#region 禁止访问特定url路径
            //app.UsePreventAccessToDirectory();
            //#endregion
            #region 允许通过url访问文件，比如loadop插件等
            //配置静态文件访问：允许通过/Files路径访问项目根目录下Files文件夹中的文件（如图片、文档等）。
            //提示：若前端部署在 Nginx，需配置 Nginx 代理该路径。
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString("/Files"), // 对外访问的路径
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")) // 指定实际物理路径
            });
            //如果前端项目部署在Nginx中（通常指服务器部署），则需要在Nginx中添加一行代理请求，否则无法正常请求。
            //location /Files/ {
            //  proxy_pass http://134.xxx.xxx.xxx:5047;
            //}
            #endregion
            #region 使用SignalR
            //映射 SignalR 的ChatHub到/ChatHubApi路径，客户端可通过该路径连接实时通信 Hub
            app.MapHub<CommonManager.SignalR.ChatHub>("/ChatHubApi");
            #endregion
            #region 注册consul
            //根据配置文件中的IsUseConsul开关，注册服务到 Consul 服务发现中心：
            //配置健康检查接口 / Api / Health / Index，返回 200 状态表示服务正常。
            //调用ConsulRegist()方法完成服务注册
            if (configuration["IsUseConsul"].ObjToBool())
            {
                app.MapWhen(context => context.Request.Path.Equals("/Api/Health/Index"),
                 applicationBuilder => applicationBuilder.Run(async context =>
                 {
                     Console.WriteLine($"This is Health Check");
                     context.Response.StatusCode = (int)HttpStatusCode.OK;
                     await context.Response.WriteAsync("OK");
                 }));//consul 注册心跳检测的接口API
                app.Configuration.ConsulRegist();
            }
            #endregion
            //app.Urls.Add(configuration["ApplicationUrl"].ToString());
            app.Run();
        }
    }
}
