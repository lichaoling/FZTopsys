using JXGIS.FZToponymy.Utils;
using JXGIS.FZToponymy.Utils.Log4net;
using JXGIS.FZToponymy.Utils.ReturnObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXGIS.FZToponymy.Utils
{
    public class CustomFilterAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// 日志描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Action执行后
        /// </summary>
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["Controller"].ToString();
            var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"].ToString();
            LogHelper.LoggerName = LoginUtils.CurrentUser != null ? LoginUtils.CurrentUser.UserId : string.Empty;
            LogHelper.UserID = LoginUtils.CurrentUser != null ? LoginUtils.CurrentUser.UserId : null;
            LogHelper.UserName = LoginUtils.CurrentUser != null ? LoginUtils.CurrentUser.UserName : null;
            if (filterContext.Exception.Message != null)
                LogHelper.Error(filterContext.Exception.Message.ToString(), actionName, controllerName, Description);
            else
                LogHelper.Info(null, actionName, controllerName, Description);
        }

        /// <summary>
        /// Action执行前
        /// </summary>
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}