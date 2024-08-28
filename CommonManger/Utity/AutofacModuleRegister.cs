using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Utity
{
    public class AutofacModuleRegister : Autofac.Module
    {
        /// <summary>
        /// 重新Autofac管道Load方法，在这里注册注入--没用
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            Assembly interfaceAssembly = Assembly.Load("Interface");
            Assembly serviceAssembly = Assembly.Load("Service");
            builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
        }
    }
}
