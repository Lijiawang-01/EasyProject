using EasyWechatModels.Dto;
using EasyWechatModels.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.IService.IBasicService
{
    public interface IRoleService
    {
        bool Add(BaseRoleReq role, string userId);
        bool Edit(BaseRoleReq role, string userId);
        bool Del(string id);
        bool BatchDel(string ids);
        PageInfo<BaseRoleRes> GetRoles(BaseRoleReq req);
        BaseRoleRes GetRoleById(string id);
    }
}
