using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Utity
{
    public class CustomerInterceptor : IInterceptor
    {
        private static Dictionary<string, object> _dic = new Dictionary<string, object>();
        public void Intercept(IInvocation invocation)
        {
            //string key = invocation.Method.Name.ToString();
            ////进入了
            //Console.WriteLine("=======进入方法" + key + "========");
            //if (_dic.ContainsKey(key))
            //{
            //    invocation.ReturnValue = _dic[key];
            //}
            //else
            //{
            //invocation.Proceed();//执行方法
            //_dic[key] = invocation.ReturnValue;
            //}
            ////方法执行完了
            //Console.WriteLine("=======执行完方法" + key + "========");
            invocation.Proceed();//执行方法
        }
    }
}
