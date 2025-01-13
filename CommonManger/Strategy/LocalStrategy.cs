using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Strategy
{
    public class LocalStrategy : FileStrategy
    {
        /// <summary>
        /// 上传到本地服务器
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public override Task<string> UploadFile(List<IFormFile> files)
        {
            var list = new List<string>();
            foreach (IFormFile file in files)
            {
                var defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "/upload/file");
                var path = Path.Combine(defaultPath, file.FileName);

                if (!System.IO.Directory.Exists(defaultPath))
                {
                    System.IO.Directory.CreateDirectory(defaultPath);//不存在就创建文件夹
                }
                using (var stream = new FileStream(path, FileMode.Create))
                {
                     file.CopyToAsync(stream);
                }
                list.Add(file.FileName);
            }
            return new Task<string>(()=> string.Join(";",list)) ;
        }
    }
}
