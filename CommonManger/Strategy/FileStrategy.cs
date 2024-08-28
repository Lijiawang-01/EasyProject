using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Strategy
{
    public abstract class FileStrategy
    {
        public abstract Task<string> UploadFile(List<IFormFile> files);
    }
}
