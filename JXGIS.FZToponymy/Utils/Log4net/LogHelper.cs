using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;


namespace JXGIS.FZToponymy.Utils.Log4net
{
    public class LogEntity
    {
        public string userID { get; set; }
        public string userName { get; set; }
        public string actionName { get; set; }
        public string controllerName { get; set; }
        public string description { get; set; }
        public string errorMessage { get; set; }
    }
    public class LogHelper
    {
        /// <summary>
        /// LoggerName
        /// </summary>
        public static string LoggerName = string.Empty;
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string UserID = string.Empty;
        /// <summary>
        /// 用户名称
        /// </summary>
        public static string UserName = string.Empty;

        //public static string ActionName = string.Empty;
        //public static string ControllerName = string.Empty;
        //public static string Description = string.Empty;
        //public static string Message = string.Empty;


        private static ILog iLog;
        private static LogEntity logEntity;

        /// <summary>
        /// 接口
        /// </summary>
        private static ILog log
        {
            get
            {
                if (iLog == null)
                {
                    iLog = log4net.LogManager.GetLogger(LoggerName);
                }
                else
                {
                    if (iLog.Logger.Name != LoggerName)
                    {
                        iLog = log4net.LogManager.GetLogger(LoggerName);
                    }
                }

                return iLog;
            }
        }

        /// <summary>
        /// 构造消息实体
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static LogEntity BuildMessageMode(string message,string actionName,string controllerName,string description)
        {
            if (logEntity == null)
            {
                logEntity = new LogEntity();
                logEntity.userID = UserID;
                logEntity.userName = UserName;
                logEntity.actionName = actionName;
                logEntity.controllerName = controllerName;
                logEntity.description = description;
                logEntity.errorMessage = message;
            }
            else
            {
                logEntity.actionName = actionName;
                logEntity.controllerName = controllerName;
                logEntity.description = description;
                logEntity.errorMessage = message;
            }

            return logEntity;
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message">消息</param>
        public static void Debug(string message, string actionName, string controllerName, string description)
        {
            if (log.IsDebugEnabled)
                log.Debug(BuildMessageMode(message, actionName,controllerName,description));
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Debug(string message, string actionName, string controllerName, string description, Exception ex)
        {
            if (log.IsDebugEnabled)
                log.Debug(BuildMessageMode(message, actionName, controllerName, description), ex);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Info(string message, string actionName, string controllerName, string description)
        {
            if (log.IsInfoEnabled)
                log.Info(BuildMessageMode(message, actionName, controllerName, description));
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Info(string message, string actionName, string controllerName, string description, Exception ex)
        {
            if (log.IsInfoEnabled)
                log.Info(BuildMessageMode(message, actionName, controllerName, description), ex);
        }

        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="message">消息</param>
        public static void Error(string message, string actionName, string controllerName, string description)
        {
            if (log.IsErrorEnabled)
                log.Error(BuildMessageMode(message, actionName, controllerName, description));
        }

        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Error(string message,string actionName, string controllerName, string description, Exception exception)
        {
            if (log.IsErrorEnabled)
                log.Error(BuildMessageMode(message, actionName, controllerName, description), exception);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">消息</param>
        public static void Warn(string message, string actionName, string controllerName, string description)
        {
            if (log.IsWarnEnabled)
                log.Warn(BuildMessageMode(message, actionName, controllerName, description));
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Warn(string message,  string actionName, string controllerName, string description,Exception ex)
        {
            if (log.IsWarnEnabled)
                log.Warn(BuildMessageMode(message, actionName, controllerName, description), ex);
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="message">消息</param>
        public static void Fatal(string message, string actionName, string controllerName, string description)
        {
            if (log.IsFatalEnabled)
                log.Fatal(BuildMessageMode(message, actionName, controllerName, description));
        }

        /// <summary>
        /// 严重
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public static void Fatal(string message, string actionName, string controllerName, string description, Exception ex)
        {
            if (log.IsFatalEnabled)
                log.Fatal(BuildMessageMode(message, actionName, controllerName, description), ex);
        }
    }
}