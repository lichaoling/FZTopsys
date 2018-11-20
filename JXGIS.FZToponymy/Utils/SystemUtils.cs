using JXGIS.FZToponymy.Models.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace JXGIS.FZToponymy.Utils
{
    public class SystemUtils
    {
        private static readonly object _lockObject1 = new object();
        private static readonly object _lockObject2 = new object();
        private static readonly object _lockObject3 = new object();

        private static dynamic _Config;
        private static ComDbContext _ComDbContext;
        private static EFDbContext _EFDbContext;

        /// <summary>
        /// 根url地址 如 http://127.0.0.1/test/subtest
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                string appPath = HttpContext.Current == null ? string.Empty : HttpContext.Current.Request.ApplicationPath;
                return appPath == "/" ? string.Empty : appPath;
            }
        }

        /// <summary>
        /// 调试配置文件地址
        /// </summary>
        private static string debugConfigPath = AppDomain.CurrentDomain.BaseDirectory + "Config\\SystemParameters.json";

        /// <summary>
        /// 发布配置文件地址
        /// </summary>
        private static string releaseConfigPath = AppDomain.CurrentDomain.BaseDirectory + "Config\\SystemParameters.txt";
        /// <summary>
        /// 配置文件内容及信息
        /// </summary>
        public static dynamic Config
        {
            get
            {
                if (_Config == null)
                {
                    lock (_lockObject1)
                    {
                        using (StreamReader sr = new StreamReader(debugConfigPath))
                        {
                            string json = sr.ReadToEnd();
                            _Config = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                        }
                        //#if DEBUG
                        //                        using (StreamReader sr = new StreamReader(debugConfigPath))
                        //                        {
                        //                            string json = sr.ReadToEnd();
                        //                            _Config = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                        //                        }
                        //#else
                        //                        using (StreamReader sr = new StreamReader(releaseConfigPath))
                        //                        {
                        //                            string json = sr.ReadToEnd();
                        //                            var key = System.Configuration.ConfigurationManager.AppSettings["tdtph"];
                        //                            json = SecurityUtils.DESDecrypt(json, key);
                        //                            _Config = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                        //                        }
                        //#endif
                    }
                }
                return _Config;
            }
        }

        /// <summary>
        /// 一般DbContext
        /// </summary>
        public static ComDbContext ComDbContext
        {
            get
            {
                if (SystemUtils._ComDbContext == null)
                    lock (_lockObject2)
                        SystemUtils._ComDbContext = new ComDbContext();
                return SystemUtils._ComDbContext;
            }
        }

        /// <summary>
        /// EntityFramework DbContext 全局不释放
        /// </summary>
        public static EFDbContext EFDbContext
        {
            get
            {
                if (SystemUtils._EFDbContext == null)
                    lock (_lockObject3)
                        SystemUtils._EFDbContext = new EFDbContext();
                return SystemUtils._EFDbContext;
            }
        }

        /// <summary>
        /// EntityFramework DbContext 非全局（每次新建） 需释放
        /// </summary>
        public static EFDbContext NewEFDbContext
        {
            get
            {
                return new EFDbContext();
            }
        }
    }
}