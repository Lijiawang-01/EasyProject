using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Strategy
{
    public class CloudStrategy : FileStrategy
    {
        /// <summary>
        /// 上传到云服务器
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task<string> UploadFile(List<IFormFile> files)
        {
            throw new NotImplementedException();
        }
    }
}
