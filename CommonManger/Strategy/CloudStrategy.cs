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
        public override Task<string> UploadFile(List<IFormFile> files)
        {
            throw new NotImplementedException();
        }
    }
}
