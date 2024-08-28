using EasyWechatModels.Other;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Helper
{
    public class ResultHelper
    {
        public static ApiResult Success(object res)
        {
            return new ApiResult() { IsSuccess = true, Result = res, Code = 200 };
        }
        public static ApiResult Error(string message, int code = 400)
        {
            return new ApiResult() { IsSuccess = false, Msg = message, Code = code };
        }
    }
}
