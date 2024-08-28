using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonManager.Utity
{
    public class DataValidationAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //bool doValid = false;
            ////判断Action是否有DoValidAttribute
            //DoValidAttribute actionAttr = (DoValidAttribute)((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.GetCustomAttribute(typeof(DoValidAttribute), false);
            //if (actionAttr != null)
            //{
            //    var attr = actionAttr as DoValidAttribute;
            //    doValid = actionAttr._doValid;
            //}
            //if (doValid)
            //{
            //    if (!context.ModelState.IsValid)
            //    {
            //        var errorMsg = this.GetErrMessage(context);
            //        context.Result = new JsonResult(new ResponseMessageDTO() { Success = false, Message = errorMsg });
            //    }
            //}
        }

        private string GetErrMessage(ActionExecutingContext context)
        {
            string smsg = "";
            foreach (var key in context.ModelState.Keys)
            {
                var errors = context.ModelState[key].Errors;
                if (errors.Count() > 0)
                {
                    smsg = errors[0].ErrorMessage;
                    break;
                }
            }
            return smsg;
        }
    }
}
