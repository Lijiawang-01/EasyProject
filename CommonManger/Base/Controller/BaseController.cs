using CommonManager.Utity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Base
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [CustomerActionFilter]    
    public class BaseController : ControllerBase
    {
    }
}
