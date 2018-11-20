using JXGIS.FZToponymy.Models.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Utils
{
    public class LoginUtils
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static IUser Login(string userName, string password)
        {
            IUser user = null;
            var b = ValidateUser(userName, password, ref user);
            CurrentUser = user;
            return user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        private static string _user = "USER";
        /// <summary>
        /// 当前用户
        /// </summary>
        public static IUser CurrentUser
        {
            get
            {
                return HttpContext.Current != null ? (HttpContext.Current.Session[_user] as IUser) : null;
            }

            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session[_user] = value;
                }
            }
        }

        /// <summary>
        /// 系统内是否有用户名为userName的用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool HasUser(string userName)
        {
            return SystemUtils.EFDbContext.SYSUSER.Where(u => u.USERNAME == userName).Count() > 0;
        }

        /// <summary>
        /// 验证用户是否合法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidateUser(string userName, string password, ref IUser user)
        {
            var bSuccess = false;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) return false;
            var us = SystemUtils.EFDbContext.SYSUSER.Where(u => u.USERNAME == userName && u.PASSWORD == password).FirstOrDefault();
            bSuccess = us == null ? false : true;
            user = us;
            return bSuccess;
        }

        /// <summary>
        /// 当前是否有用户
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                return CurrentUser == null;
            }
        }


        private static string _validateGraphicCode = "VALIDATEGRAPHICCODE";

        private static ValidateGraphic validateGraphic = new ValidateGraphic();
        /// <summary>
        /// 当前验证码
        /// </summary>
        public static ValidateGraphic CurrentValidateGraphicCode
        {
            get
            {
                if (validateGraphic == null)
                {
                    validateGraphic = new ValidateGraphic();
                }
                return validateGraphic;
            }

            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session[_validateGraphicCode] = value;
                }
            }
        }
    }
}