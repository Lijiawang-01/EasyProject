using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace EasyWechatModels.Other
{
    public class WebApiResponse
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public object Result { get; set; }
    }

    public class WebApiResponse<T>
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public T Result { get; set; }
    }
    public class BaseRequest
    {
        public Method Method { get; set; }
        public string Route { get; set; }
        public string ContenType { get; set; } = "application/json";
        public string Parameter { get; set; }
    }
}
