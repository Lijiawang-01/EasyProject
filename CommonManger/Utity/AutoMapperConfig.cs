using AutoMapper;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Utity
{
    /// <summary>
    /// AutoMapper配置映射
    /// </summary>
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            ////角色
            //CreateMap<BaseRole, BaseRoleRes>();
            //CreateMap<BaseRoleReq, BaseRole>();
            ////用户
            //CreateMap<BaseUsers, BaseUsersRes>();
            //CreateMap<BaseUsersReq, BaseUsers>();
            ////菜单
            //CreateMap<BaseMenu, BaseMenuRes>();
            //CreateMap<BaseMenuReq, BaseMenu>();
            ////地址
            //CreateMap<BaseArea, BaseAreaRes>();
            //CreateMap<BaseAreaReq, BaseArea>();
            //使用反射注册
            RegisterEasyWechatModelsMappings();
        }
        private void RegisterEasyWechatModelsMappings()
        {
            // 假设所有相关类型都在 EasyWechatModels 命名空间下
            string path = AppContext.BaseDirectory;
            var dtoAssembly = Assembly.LoadFrom(path+"EasyWechatModels.dll");

            // 获取所有实体类（如 BaseRole、BaseUsers 等）
            var entityTypes = dtoAssembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith("EasyWechatModels.Entitys"))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                // 1. 注册 Entity -> Res
                var resType = dtoAssembly.GetType($"EasyWechatModels.Dto.{entityType.Name}Res");
                if (resType != null)
                {
                    CreateMap(entityType, resType);
                }

                // 2. 注册 Req -> Entity
                var reqType = dtoAssembly.GetType($"EasyWechatModels.Dto.{entityType.Name}Req");
                if (reqType != null)
                {
                    CreateMap(reqType, entityType);
                }
            }
        }
    }
}
