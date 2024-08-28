using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.RestSharp
{
    /// <summary>
    /// 封装的接收REST返回结果
    /// 现有的同步工程返回值用的是APIResult
    /// </summary>
    /// <typeparam name="T">返回结果中的值类型</typeparam>
    public class ResponseMessage<T>
    {
        /**
         * 是否成功
         */
        public Boolean success;

        /**
         * 反馈数据
         */
        public T content;

        /**
         * 反馈信息
         */
        public String message;

        /**
         * 响应码
         */
        public int code;
    }
}
