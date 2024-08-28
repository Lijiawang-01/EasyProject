using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasyWechatModels.Enum.AppEnum;

namespace CommonManager.Strategy
{
    public class FileFactory
    {
        public static FileStrategy CreateFileStrategy(UploadModeEnum type)
        {
            switch (type)
            {
                case UploadModeEnum.Loacl:
                    return new LocalStrategy();
                case UploadModeEnum.Cloud:
                    return new CloudStrategy();
                default:
                    return new LocalStrategy();
            }
        }
    }
}
