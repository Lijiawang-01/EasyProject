using CommonManager.Base.IService;
using CommonManager.Strategy;
using EasyWechatModels.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Base.Service
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFile(List<IFormFile> files, AppEnum.UploadModeEnum uploadMode)
        {
            var fileContext = new FileContext(FileFactory.CreateFileStrategy(uploadMode), files);
            return await fileContext.ContextInterface();
        }
    }
}
