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
            IConfiguration configuration = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.json", false, true)
                          .Build();
            builder.Services.AddSingleton(new AppSettingHelper(configuration));
            #endregion
            // Add services to the container.
            #region 注册userInfo中的HttpContext
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            UserInfoHelper.serviceCollection = builder.Services;
            #endregion
            #region swagger&跨域
            //注册swagger
            builder.Services.AddSwaggerExt();
            //注册跨域
            builder.Services.AddCorsExt();
            #endregion
            //builder.Services.InitService();
            #region 禁用数据验证
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // 使用自定义模型验证【Api接口需要添加才能生效】
            });
            #endregion
            #region 使用Session 
            //Session的服务端存储需要缓存，所以需要引入.Net core的缓存DistributedMemoryCache；
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Domain = "localhost";
                options.Cookie.Name = "EasyWechat.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(2000);//设置session的过期时间
                options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值 
            });
            SessionHelper.serviceCollection = builder.Services;
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
            var config = new MapperConfiguration(e => e.AddProfile(new AutoMapperConfig()));
            builder.Services.AddSingleton(config);
            //注册mapper映射
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            #endregion
            #endregion
            #region mvc
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
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
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
                    // 获取到 Autofac 的容器,静态调用方法
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
            builder.Services.AddSignalRSetup();
            #endregion
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
            else
                #endregion
                #region 使用跨域
                //跨域
                app.UseCorsExt();
            #endregion
            //app.UseAuthorization();
            #region 使用session
            app.UseSession();
            #endregion
            app.MapControllers();
            #region 错误中间件
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            #endregion
            //#region 禁止访问特定url路径
            //app.UsePreventAccessToDirectory();
            //#endregion
            #region 允许通过url访问文件，比如loadop插件等
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
            app.MapHub<CommonManager.SignalR.ChatHub>("/ChatHubApi");
            #endregion
            #region 注册consul
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
