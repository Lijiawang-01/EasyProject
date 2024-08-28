using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Error
{
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
