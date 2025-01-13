using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Error
{
    /// <summary>
    /// 错误返回结果对象
    /// </summary>
    public class ErrorActionResult
    {
        public string? SuccesDesc { get; set; }
        public SuccesTypeEnum Succes { get; set; }
        public string? ErrorMsg { get; set; }
        public object? Data { get; set; }
    }
    public enum SuccesTypeEnum
    {
        Error=0,
        Success=1,
    }
}
