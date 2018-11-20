using JXGIS.FZToponymy.Utils;
using JXGIS.FZToponymy.Utils.Log4net;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXGIS.FZToponymy.Controllers
{

    public class HomeController : Controller
    {
        //private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
         
        // GET: Home
        [CustomFilter(Description = "测试")]
        [CustomHandleError]
        public ActionResult Index()
        {
            throw new Exception("有错误");

            return View();
        }
    }
}