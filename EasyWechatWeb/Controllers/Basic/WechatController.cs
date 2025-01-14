using CommonManager.Helper;
using CommonManager.SwaggerExtend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyWechat.WebApi.Controllers.Basic
{
    /// <summary>
    /// 微信控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.WeChat))]
    public class WechatController : ControllerBase
    {
        private string imgPath = AppSettingHelper.ReadAppSettings("ImgPath").ToString();
        /// <summary>
        /// 菜单栏
        /// </summary>
        public class TooBar
        {
            public string name { get; set; }
            public string url { get; set; }
            public int id { get; set; }
        }
        /// <summary>
        /// 获取菜单栏图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TooBar> GetTooBarsImg()
        {
            var list=new List<TooBar>();
            for (int i = 1; i < 4; i++)
            {
                list.Add(new TooBar { name = "测试" + i, url = i + ".png",id=i });
            }
            return list;
        }
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetImage(int id)
        {
            // 根据传入的 ID 查询数据库或其他存储位置获取对应的图片文件路径

            string imagePath = $"{imgPath}\\{id}.png";
            if (System.IO.File.Exists(imagePath))
            {
                var fileStream = System.IO.File.OpenRead(imagePath);

                return File(fileStream, "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// 获取菜单栏
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TooBar> GetTooBars()
        {
            var list = new List<TooBar>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new TooBar { name = "菜单" + i, url = i + ".png", id = i });
            }
            return list;
        }
    }
}
