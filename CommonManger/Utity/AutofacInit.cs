using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Utity
{
    public static class AutofacInit
    {
        private static IContainer _container;

        public static void InitContainer(IContainer container)
        {
            _container = container;
        }

        private static IDictionary<string, string> _epsRegisteredTypes;
        /// <summary>
        /// 在EPS中注册的所有类型列表，包括DAO和BLO
        /// </summary>
        public static IDictionary<string, string> EpsRegisteredTypes
        {
            get
            {
                if (_epsRegisteredTypes == null)
                {
                    _epsRegisteredTypes = new Dictionary<string, string>();
                }

                return _epsRegisteredTypes;
            }

            set
            {
                _epsRegisteredTypes = value;
            }
        }

        /// <summary>
        /// 从容器中获取对象
        /// 示例AutofacInit.GetFromFac<IFreeSql>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetServiceFromFac<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// 从autoFac对象容器中获取已注入的对象
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static object Resolve(Type serviceType)
        {
            object result = _container.Resolve(serviceType);

            return result;
        }

        /// <summary>
        /// 从autoFac对象容器中获取已注入的泛型接口对象
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            var result = _container.Resolve<T>();

            return result;
        }

        /// <summary>
        /// 从AutoFac对象容器中获取已注入的对象(有别名)
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static object Resolve(Type serviceType, string aliasName)
        {
            object result = _container.ResolveNamed(aliasName, serviceType);

            return result;
        }
    }
}
