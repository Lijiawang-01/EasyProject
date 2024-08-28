using BusinessManager.IService.IBasicService;
using CommonManager.Helper;
using CommonManager.SwaggerExtend;
using EasyWechatModels.Entitys;
using EasyWechatWeb;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using NPOI.SS.Formula.Functions;
using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Text;
using ZstdSharp.Unsafe;

namespace EasyWechat.WebApi.Controllers.Basic
{
    /// <summary>
    /// 工具方法
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ApiVersionInfo.System))]
    public class ToolController : ControllerBase
    {
        public ISqlSugarClient _db { get; set; }
        public IDicService _DicService { get; set; }
        public IDicItemService _DicItemService { get; set; }
        /// <summary>
        /// 初始化数据库
        /// initCount=0 跟据类生成表
        /// initCount=1 需要初始化数据
        /// initCount=2 初始化数据库和数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string InitDateBase(int initCount = 0)
        {
            string res = "OK";
            //如果不存在则创建数据库
            if (initCount == 2)
            {
                _db.DbMaintenance.CreateDatabase();
            }
            //创建表
            string nspace = "EasyWechatModels.Entitys";
            //通过反射读取我们想要的类
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "EasyWechatModels.dll").GetTypes().Where(p => p.Namespace.Contains(nspace)).ToArray();
            _db.CodeFirst.SetStringDefaultLength(255).InitTables(ass);
            //初始化炒鸡管理员和菜单
            #region 初始化数据
            if (initCount == 1 || initCount == 2)
            {
                BaseUsers user = new BaseUsers()
                {
                    Name = "admin",
                    NickName = "管理员",
                    Password = "123456",
                    UserType = 0,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的管理员",
                    CreateDate = DateTime.Now,
                    CreateUserId = Guid.Empty.ToString(),
                    IsDeleted = 0
                };
                string userId = _db.Insertable(user).ExecuteReturnEntity().Id;
                BaseMenu menuRoot = new BaseMenu()
                {
                    Name = "菜单管理",
                    Index = "menumanager",
                    FilePath = "../views/admin/menu/MenuManager",
                    ParentId = Guid.Empty.ToString(),
                    Order = 0,
                    IsEnable = true,
                    Description = "数据库初始化时默认添加的默认菜单",
                    CreateDate = DateTime.Now,
                    CreateUserId = userId,
                    IsDeleted = 0
                };
                _db.Insertable(menuRoot).ExecuteReturnEntity();
            }
            #endregion
            return res;
        }
        /// <summary>
        /// 初始化AppEnum中的枚举到字典表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string InitAppEnumBase()
        {
            string res = "OK";
            var ss = EnumHelper.GetEnumValueDescriptionList<EasyWechatModels.Enum.AppEnum.StatusEnum>();
            var appEnum = Assembly.LoadFrom(AppContext.BaseDirectory + "EasyWechatModels.dll").GetType(" EasyWechatModels.Enum.AppEnum");
            var dicList = new List<BaseDictionary>();
            var itemList = new List<BaseDictionaryItem>();
            if (appEnum != null)
            {
                // 获取类中所有的嵌套类型（包括枚举）
                Type[] nestedTypes = appEnum.GetNestedTypes();
                // 过滤出枚举类型
                Type[] enumTypes = nestedTypes.Where(t => t.IsEnum).ToArray();
                // 输出所有枚举类型的名称
                foreach (Type enumType in enumTypes)
                {

                    var dic = new BaseDictionary();
                    dic.DicCode = enumType.Name;
                    object[] attr1 = enumType.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    string description1 = attr1.Length == 0 ? enumType.Name : ((DescriptionAttribute)attr1[0]).Description;
                    dic.DicName = description1;
                    dicList.Add(dic);
                    FieldInfo[] fields = enumType.GetFields();
                    int index = 0;
                    foreach (FieldInfo field in fields)
                    {
                        if (field.FieldType.IsEnum)
                        {
                            object[] attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                            string description = attr.Length == 0 ? field.Name : ((DescriptionAttribute)attr[0]).Description;
                            var dicItem = new BaseDictionaryItem();
                            dicItem.ItemName = description;
                            dicItem.ItemValue = (int)field.GetValue(null);
                            dicItem.DispalyOrder = index + 1;
                            dicItem.DictionaryId = dic.Id;
                            itemList.Add(dicItem);
                            index++;
                        }
                    }
                }
                _DicService.AddList(dicList);
                _DicItemService.AddList(itemList);
            }
            return JsonHelper.ToJson(dicList);
        }
        /// <summary>
        /// 跟据类型名称创建数据库表
        /// tableName是类型名称
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet]
        public string InitDateBaseByName(string tableName)
        {
            string res = "OK";
            //创建表
            string nspace = "EasyWechatModels.Entitys";
            //通过反射读取我们想要的类
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "EasyWechatModels.dll").GetTypes().Where(p => p.Namespace.Contains(nspace) && p.Name == tableName).ToArray();
            _db.CodeFirst.InitTables(ass);
            return res;
        }
        /// <summary>
        /// datafirst
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string InitEntity()
        {
            try
            {
                _db.AsTenant().GetConnection(1).DbFirst.IsCreateAttribute().Where(it => it.ToLower() == "base_workflow_tmp_child").CreateClassFile(@"D:\Work\easy-project\EasyProjectCore\EasyWechatModels\Entitys\Workflow", "EasyWechatModels.Entitys.Workflow");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";
        }
        /// <summary>
        /// 测试日志功能
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string TestLog()
        {
            try
            {
                LogHelper.Info("info测试");
                LogHelper.Error("Error测试");
                LogHelper.MysqlError("MysqlError测试");
                LogHelper.MysqlInfo("MysqlInfo测试");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";
        }

        /// <summary>
        /// 初始化mapper对象
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string InitDateBaseDto(int initCount = 0)
        {
            string res = "OK";
            string nspace = "EasyWechatModels.Entitys";
            //通过反射读取我们想要的类
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "EasyWechatModels.dll").GetTypes().Where(p => p.Namespace.Contains(nspace)).ToArray();
            string dtoDirectory = AppSettingHelper.ReadAppSettings("DtoDirectory");
            foreach (var item in ass)
            {
                var name = item.Name;
                // 构建完整路径
                string folderPath = Path.Combine(dtoDirectory, name);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string destinationFilePath = Path.Combine(folderPath, $"{name}Res.cs");

                string destination2FilePath = Path.Combine(folderPath, $"{name}Req.cs");

                // 复制文件到目标文件夹
                if (!System.IO.File.Exists(destinationFilePath))
                {
                    string filePath = Path.Combine(folderPath, $"{name}Res.cs");
                    StringBuilder sb = new StringBuilder();
                    // 使用StreamWriter类创建一个文件并写入数据

                    sb.AppendLine("using System;");
                    sb.AppendLine("using System.Collections.Generic;");
                    sb.AppendLine("using System.Linq;");
                    sb.AppendLine("using System.Text;");
                    sb.AppendLine("using System.Threading.Tasks;");
                    sb.AppendLine("\n");
                    sb.AppendLine("namespace EasyWechatModels.Dto");
                    sb.AppendLine("{");
                    sb.AppendLine("public class " + name + "Res{");
                    Type Objass = Assembly.LoadFrom(AppContext.BaseDirectory + "EasyWechatModels.dll").GetTypes().Where(p => p.Namespace.Contains(nspace) && p.Name == name).FirstOrDefault();
                    var properties = Objass.GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var pname = properties[i].Name;
                        var pType = "";
                        if (pType.StartsWith("List"))
                            continue;
                        if (pType.StartsWith("Nullable"))
                            continue;
                        if (properties[i].PropertyType == typeof(System.Int32))
                            pType = "int";
                        if (properties[i].PropertyType == typeof(System.DateTime))
                            pType = "DateTime";
                        if (properties[i].PropertyType == typeof(System.Double))
                            pType = "double";
                        if (properties[i].PropertyType == typeof(System.Boolean))
                            pType = "bool";
                        if (properties[i].PropertyType == typeof(System.String))
                            pType = "string";
                        if (pType == "") continue;
                        sb.AppendLine("public " + pType + " " + pname + " {get;set;}");
                    }
                    sb.AppendLine("}");
                    sb.AppendLine("}");
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(sb.ToString());
                    }
                }
                if (!System.IO.File.Exists(destination2FilePath) && System.IO.File.Exists(destinationFilePath))
                {

                    // 读取内容
                    string content = System.IO.File.ReadAllText(destinationFilePath).Replace($"{name}Res",$"{name}Req");

                    // 将内容写入，如果不存在则会自动创建
                    System.IO.File.WriteAllText(destination2FilePath, content);
                }
            }

            return res;
        }
    }
}
