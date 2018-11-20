using JXGIS.FZToponymy.Utils.ReturnObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXGIS.FZToponymy.Utils
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                RtObj rt = new RtObj(filterContext.Exception);
                var s = Newtonsoft.Json.JsonConvert.SerializeObject(rt);
                filterContext.Result = new ContentResult() { Content = s };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}