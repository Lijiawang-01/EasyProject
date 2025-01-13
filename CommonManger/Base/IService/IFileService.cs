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
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="files">文件集合</param>
        /// <param name="uploadMode">文件上传模式</param>
        /// <returns></returns>
        Task<string> UploadFile(List<IFormFile> files, UploadModeEnum uploadMode);
    }
}
