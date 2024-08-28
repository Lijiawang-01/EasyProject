using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyWechatModels.Dto
{
    public class SystemLogReq
    {
        public DateTime LogDate { get; set; }
        public string LogThread { get; set; }
        public string LogLevel { get; set; }
        public string logLogger { get; set; }
        public string LogMessage { get; set; }
    }
}
