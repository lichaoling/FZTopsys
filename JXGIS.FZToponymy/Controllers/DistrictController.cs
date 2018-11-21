using JXGIS.FZToponymy.Utils;
using JXGIS.FZToponymy.Utils.ReturnObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXGIS.FZToponymy.Controllers
{
    public class DistrictController : Controller
    {
        public ActionResult GetDistrictTree()
        {
            var data = PriviligeUtils.GetDistrictTree(LoginUtils.CurrentUser.UserId);
            RtObj rt = new RtObj(data);
            var s = Newtonsoft.Json.JsonConvert.SerializeObject(rt);
            return Content(s);
        }
    }
}