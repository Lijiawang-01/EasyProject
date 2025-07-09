using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using CommonManager.Cache;
using CommonManager.Helper;
using CommonManager.SqlSugar;
using CommonManager.Utity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    internal class CommInject
    { /// <summary>
      /// 初始化容器
      /// </summary>
      /// <returns></returns>
        public static IContainer InitIoc()
        {
            IConfiguration configuration = null;
            #region 获取web项目的appsettings.json文件路径
            //            //获取web项目的appsettings.json文件路径
            //            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //            string bin = currentDirectory.Substring(0, currentDirectory.LastIndexOf("bin"));
            //            bin = bin.TrimEnd('\\');

            //            //解决方案目录
            //            string slnProject = Directory.GetParent(bin).FullName;

            //            var dir = Directory.GetDirectories(slnProject);
            //            string? configJsonfilePath = null;
            //            foreach (var item in dir)
            //            {
            //                var files = Directory.GetFiles(item);
            //                if (files.Length == 0)
            //                {
            //                    continue;
            //                }
            //                string jsonName = $"appsettings.Development.json";
            //#if (!DEBUG)
            //                jsonName = $"appsettings.Staging.json";
            //#endif
            //                LogHelper.Info("配置文件=" + jsonName);
            //                configJsonfilePath = files.FirstOrDefault(g => g.Contains(jsonName));
            //                if (!string.IsNullOrWhiteSpace(configJsonfilePath))
            //                {
            //                    break;
            //                }
            //            }

            //            //加载配置文件
            //            configuration = new ConfigurationBuilder()
            //          .AddJsonFile(configJsonfilePath)
            //            .Build();
            #endregion

            configuration = new ConfigurationBuilder()
                         .AddJsonFile("appsettings.json", false, true)
                         .Build();
            ServiceCollection serviceCollection = new ServiceCollection();
            //ConfigureServices.AddServices(serviceCollection);
            serviceCollection.AddSingleton(new AppSettingHelper(configuration));
            serviceCollection.AddSqlsugarSetup();
            serviceCollection.AddLogging(cfg =>
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

            #region 缓存
            if (configuration["IsUseRedis"].ObjToBool())
            {
                //使用redis缓存
                serviceCollection.AddSingleton<IRedisCache, RedisCache>();
            }
            else
            {
                //使用内存缓存
                serviceCollection.AddSingleton<IMemoryCache, MemoryCache>();
            }
            #endregion
            #region 注册autoMapper
            //注册mapper
            var config = new MapperConfiguration(e => e.AddProfile(new AutoMapperConfig()));
            serviceCollection.AddSingleton(config);
            //注册mapper映射
            serviceCollection.AddAutoMapper(typeof(AutoMapperConfig));
            #endregion
            //实例化 AutoFac  容器  使用反射获取所有实现接口的类注入DI容器
            //实例化Autofac容器
            var containerBuilder = new ContainerBuilder();
            #region autofac+Castle
            {
                containerBuilder.RegisterType<CustomerInterceptor>();
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
                        .AsImplementedInterfaces()
                        .InstancePerDependency()
                        .PropertiesAutowired();
                }
                #endregion
            }
            //将services中的服务填充到Autofac 的容器中
            containerBuilder.Populate(serviceCollection);
            #endregion
            //使用已进行的组件登记创建新容器
            //第三方IoC容器接管Core内置DI容器 
            var ApplicationContainer = containerBuilder.Build();
            return ApplicationContainer;
        }
    }
}
