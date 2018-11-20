using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace JXGIS.FZToponymy.Utils
{
    public class SessionUtils
    {
        public static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Session;
                }
                else
                {
                    throw new Exception("当前无会话！");
                }
            }
        }

        public static void Set(string name, object value)
        {
            if (HasKey(name))
            {
                Session[name] = value;
            }
            else
            {
                Session.Add(name, value);
            }
        }

        public static object Get(string name)
        {
            return Session[name];
        }

        public static bool HasKey(string key)
        {
            return Session[key] != null;
        }
    }
}