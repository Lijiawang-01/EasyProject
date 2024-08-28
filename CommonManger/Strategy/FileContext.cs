using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Strategy
{
    /// <summary>
    /// 文件上下文，调用具体的策略
    /// </summary>
    public class FileContext
    {
        private FileStrategy _strategy;

        private List<IFormFile> _files;
        public FileContext(FileStrategy strategy, List<IFormFile> files)
        {
            _strategy = strategy;
            _files = files;
        }
        public async Task<string> ContextInterface()
        {
            return await _strategy.UploadFile(_files);
        }
    }
}
