using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasyWechatModels.Enum.AppEnum;

namespace CommonManager.Base.IService
{
    public interface IFileService
    {
        Task<string> UploadFile(List<IFormFile> files, UploadModeEnum uploadMode);
    }
}
