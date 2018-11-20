using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Utils.ReturnObj
{
    /// <summary>
    /// 默认的错误处理器
    /// </summary>
    public class DefaultRtObjCfg : IRtObjCfg
    {
        /// <summary>
        /// 从App config中获取配置，确保app config中有name为defaultErrorMessage的节，没有的话提供默认值
        /// </summary>
        /// <returns></returns>
        public string GetDefaultErrorMessage()
        {
            string defaultErrorMessage = System.Configuration.ConfigurationManager.AppSettings["defaultErrorMessage"];
            return string.IsNullOrEmpty(defaultErrorMessage) ? "处理数据的过程中发生了错误，请联系系统管理员！" : defaultErrorMessage;
        }

        /// <summary>
        /// 从App config中获取配置，确保app config中有name为showOrginErrorMessage的节，没有的话始终为false，showOrginErrorMessage的值只能为"true"（返回true）和其它值（返回false
        /// ）
        /// </summary>
        /// <returns></returns>
        public bool GetShowOrginErrorMessage()
        {
            return System.Configuration.ConfigurationManager.AppSettings["showOrginErrorMessage"] == "true";
        }

        /// <summary>
        /// 在运行目录下新增Log文件夹，并以日期生成日志文件，将错误信息写入日志
        /// System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase获取该应用程序的目录的名称。
        /// </summary>
        /// <param name="ex"></param>
        public void Log(Exception ex)
        {
            var logFilePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Log","SystemLogInfo");

            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }
            var fileFullPath = Path.Combine(logFilePath, DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs);

                /// 错误格式
                string esm = string.Format(@"======= {0}{3}|Message:{1}{3}|StackTrace:{2}{3}", DateTime.Now.ToString(), ex.Message, ex.StackTrace, System.Environment.NewLine);
                sw.Write(esm);
            }
            catch
            {

            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }
    }
}