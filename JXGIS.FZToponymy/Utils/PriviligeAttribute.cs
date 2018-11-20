using JXGIS.FZToponymy.Models.Domain;
using JXGIS.FZToponymy.Utils.ReturnObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JXGIS.FZToponymy.Utils
{
    public class DescriptionAttribute : Attribute
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class PriviligeAttribute : AuthorizeAttribute
    {
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 全局调试状态下是否开启授权，默认关闭
        /// </summary>
        public static bool DebugAuthorize { get; set; } = false;

        private static List<SYSPRIVILIGE> priviligesInDb = PriviligeUtils.PriviligesInDatabase;

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
#if DEBUG
            if (DebugAuthorize == false) return;
#endif
            var user = LoginUtils.CurrentUser as SYSUSER;
            var controller = filterContext.RouteData.Values["controller"].ToString().ToUpper();
            var action = filterContext.RouteData.Values["action"].ToString().ToUpper();
            /* 通过数据库中的权限得知其是否需要验证，如果需要验证，则查看用户是否有该权限 */
            var privilige = priviligesInDb.Where(p => p.ACTION.ToUpper() == action && p.CONTROLLER.ToUpper() == controller).FirstOrDefault();
            if (privilige != null && privilige.NEEDAUTHORIZE == true)
            {
                /* 1、用户未登录，根据权限类型（数据、页面）决定返回错误还是返回登录页面；
                 * 2、用户已登录，但没有权限，根据权限类型决定跳转到权限不足提示页面或返回错误 */
                if (user == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest()/*privilige.TYPE == PriviligeUtils.ActionResultType.Data*/)   //请求数据
                    {
                        filterContext.Result = new JsonResult() { Data = new RtObj(new Error("未登录，请求数据")) };
                    }
                    else       //请求页面
                    {
                        //string url = string.Format("{0}?target={1}", SystemUtils.Config.SystemPages.LoginPage, System.Web.HttpContext.Current.Request.Url);
                        //filterContext.Result = new RedirectResult(url);
                        filterContext.Result = new JsonResult() { Data = new RtObj(new Error("未登录，请求页面，" + System.Web.HttpContext.Current.Request.Url.ToString())) };
                    }
                }
                else if (user.Priviliges.Where(p => p.CONTROLLER.ToUpper() == controller && p.ACTION.ToUpper() == action).Count() == 0)
                {
                    if (privilige.TYPE == PriviligeUtils.ActionResultType.Data)   //请求数据
                    {
                        filterContext.Result = new JsonResult() { Data = new RtObj(new Error("已登录，请求数据，没有足够的权限")) };
                    }
                    else   //请求页面
                    {
                        //filterContext.Result = new RedirectResult(SystemUtils.Config.SystemPages.NotEnoughAuthorityPage.ToString());
                        filterContext.Result = new JsonResult() { Data = new RtObj(new Error("已登录，请求页面，没有足够的权限")) };
                    }
                }
            }
        }
    }
}