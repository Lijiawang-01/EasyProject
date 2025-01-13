using AutoMapper;
using EasyWechatModels.Dto;
using EasyWechatModels.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            //角色
            CreateMap<BaseRole, BaseRoleRes>();
            CreateMap<BaseRoleReq, BaseRole>();
            //用户
            CreateMap<BaseUsers, BaseUsersRes>();
            CreateMap<BaseUsersReq, BaseUsers>();
            //菜单
            CreateMap<BaseMenu, BaseMenuRes>();
            CreateMap<BaseMenuReq, BaseMenu>();
            //地址
            CreateMap<BaseArea, BaseAreaRes>();
            CreateMap<BaseAreaReq, BaseArea>();
        }
    }
}
