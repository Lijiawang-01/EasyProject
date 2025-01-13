using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Utity
{
    public class CustomeInterceptorSelector : IInterceptorSelector
    {
        /// <summary>
        /// Castle选择拦截器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="interceptors"></param>
        /// <returns></returns>
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            //return interceptors;
            return new IInterceptor[]
            {
                new CustomerInterceptor()
            };
        }
    }
}
