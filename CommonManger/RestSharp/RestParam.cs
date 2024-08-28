using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.RestSharp
{
    public class RestParam
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public EmParType ParamType { get; set; }
    }

    public enum EmParType
    {
        UrlSegment,
        Param,
        Body
    }
}
