using BusinessManager.IService.IBasicService;
using CommonManager.Base.IService;
using CommonManager.Base.Service;
using CommonManager.Cache;
using CommonManager.Helper;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EasyWechat.WebApi.Controllers.Basic
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.Test))]
    public class TestController : ControllerBase
    {
        public IUserService _userService { get; set; }
        public IMemoryCache _memoryCache { get; set; }
        public IRedisCache _redisCache { get; set; }
        public IFileService _fileService { get; set; }
        public IAreaService _areaService { get; set; }
        /// <summary>
        /// 测试Session
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Test()
        {
            ////错误中间件
            //_redisCache.SetCache("ss", "asd");
            //_memoryCache.SetCache("ss", "asd");
            var sestr = SessionHelper.GetSession("ces");
            var uservice = CommonManager.Utity.AutofacInit.Resolve<IUserService>();
            var obj = uservice.GetUser("admin", "123456");
            var obj2 = _userService.GetById(1);
            var str = AppSettingHelper.ReadAppSettings("Test", "A");
            SessionHelper.SetSession("ces", "as");
            var str2 = _memoryCache.GetCache<string>("ss");
            return "OK";
        }
        /// <summary>
        /// 导出excel测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string TestExcel()
        {
            var objList = _areaService.GetList();
            NpoiHepler.ExportExcel(objList, "地址列表");
            return "OK";
        }
        /// <summary>
        /// 导出excel测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string TestExportAreas()
        {
            var objList = _areaService.GetList();
            NpoiHepler.ExportExcel(objList, "地址列表");
            return "OK";
        }
        /// <summary>
        ///  导出excel测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string TestChangeArea()
        {
            var objList = _userService.GetList();
            NpoiHepler.ExportExcel(objList, "用户列表");
            return "OK";
        }
        /// <summary>
        /// 导入excel测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> TestImportExcel(IFormFile file)
        {
            var defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "/upload/file");
            var path = Path.Combine(defaultPath, file.FileName);

            if (!System.IO.Directory.Exists(defaultPath))
            {
                System.IO.Directory.CreateDirectory(defaultPath);//不存在就创建文件夹
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var dt = NpoiHepler.ReadExcelToDataTable(path);
            var list = new List<BaseAreaReq>();
            if (dt != null)
            {
                foreach (DataRow area in dt.Rows)
                {
                    var item = new BaseAreaReq();
                    item.AreaName = area["名称"] + "";
                    item.AreaCode = area["编码"] + "";
                    item.Id = Guid.NewGuid() + "";
                    item.ParentId = area["父节点"] + "";
                    item.AreaLevel = Convert.ToInt32(area["等级"] + "");
                    item.AreaPath = item.AreaName;
                }
            }
            return Ok();
        }
        /// <summary>
        /// 保存文件测试
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "/upload/file");
            var path = Path.Combine(defaultPath, file.FileName);

            if (!System.IO.Directory.Exists(defaultPath))
            {
                System.IO.Directory.CreateDirectory(defaultPath);//不存在就创建文件夹
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var dt = NpoiHepler.ReadExcelToDataTable(path);
            var list = new List<BaseAreaReq>();
            if (dt != null)
            {
                foreach (DataRow area in dt.Rows)
                {
                    var item = new BaseAreaReq();
                    item.AreaName = area["名称"] + "";
                    item.NodeId = area["节点"] + "";
                    item.ParentNodeId = area["父节点"] + "";
                    item.Id = Guid.NewGuid() + "";
                    item.ParentId = Guid.Empty + "";
                    item.AreaLevel = Convert.ToInt32(area["等级"] + "");
                    item.AreaPath = item.AreaName;
                    list.Add(item);
                }
            }
            var list2 = list;
            var list3 = new List<BaseAreaReq>();
            foreach (var item in list)
            {
                if (item.ParentNodeId != "0")
                {
                    var parentItem = list2.Where(x => x.NodeId == item.ParentNodeId).FirstOrDefault();
                    if (parentItem != null)
                        item.ParentId = parentItem.Id;
                }
                string path2 = item.AreaName;
                if (item.AreaLevel==2)
                {
                    var parentItem = list2.Where(x => x.NodeId == item.ParentNodeId).FirstOrDefault();
                    path2 = parentItem.AreaName +","+ path2;
                }
                if (item.AreaLevel == 3)
                {
                    var parentItem = list2.Where(x => x.NodeId == item.ParentNodeId).FirstOrDefault();
                    var parentItem2 = list2.Where(x => x.NodeId == parentItem.ParentNodeId).FirstOrDefault();
                    path2 = parentItem2.AreaName + "," + parentItem.AreaName + "," + path2;
                }
                item.AreaPath = path2;
                list3.Add(item);
                _areaService.Add(item);
            }
            return Ok(list3);
        }
    }
}
