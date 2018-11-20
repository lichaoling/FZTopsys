using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace JXGIS.FZToponymy.Utils
{
    public class ValidateUtils
    {
        public class Pattern
        {
            /// <summary>
            /// 密码
            /// </summary>
            public const string Password = @"^[a-zA-Z]\w{5,15}$";
            /// <summary>
            /// 强密码
            /// </summary>
            public const string StrongPassword = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$";
            /// <summary>
            /// 邮箱
            /// </summary>
            public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            /// <summary>
            /// 用户名
            /// </summary>
            public const string UserName = "^[a-zA-Z][a-zA-Z0-9_]{4,15}$";
            /// <summary>
            /// 电话
            /// </summary>
            public const string Telephone = @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$";
            /// <summary>
            /// 电话
            /// </summary>
            public const string Phone = @"\d{3}-\d{8}|\d{4}-\d{7}";
            /// <summary>
            /// 身份证
            /// </summary>
            public const string ID = @"^\d{15}|\d{18}$";

        }
        public static bool Validate(string text, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(text);
        }

        public static bool ValidateTelephone(string telephone)
        {
            var b = false;
            if (!string.IsNullOrWhiteSpace(telephone))
            {
                telephone = string.Join(string.Empty, telephone.Split('-'));
                b = Validate(telephone, ValidateUtils.Pattern.Telephone);
            }
            return b;
        }

        public static bool ValidateEmail(string email)
        {
            return Validate(email, ValidateUtils.Pattern.Email);
        }

        public static bool ValidatePassword(string password)
        {
            return Validate(password, ValidateUtils.Pattern.Password);
        }

        public static bool ValidateUserName(string userName)
        {
            return Validate(userName, ValidateUtils.Pattern.UserName);
        }
    }
}