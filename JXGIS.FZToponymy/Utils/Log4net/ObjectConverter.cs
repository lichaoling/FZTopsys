using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;
using log4net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace JXGIS.FZToponymy.Utils.Log4net
{
    /// <summary>
    /// 根据键值获取值的对象
    /// </summary>
    public interface IGetObjectValueByKey
    {
        string GetByKey(string name);
    }

    public class ObjectConverter : PatternLayoutConverter
    {
        static Func<object, string, object> funcs;
        static ObjectConverter()
        {
            //********根据键值获取值的顺序
            //从接口获取值
            funcs += GetValueByInterface;
            //反射获取属性值
            funcs += GetValueByReflection;
            //从索引值获取值
            funcs += GetValueByIndexer;
        }

        /// <summary>
        /// 实现PatternLayoutConverter.Convert抽象方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="loggingEvent"></param>
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            //获取传入的消息对象
            object objMsg = loggingEvent.MessageObject;

            if (objMsg == null)
            {
                //如果对象为空输出log4net默认的null字符串
                writer.Write(SystemInfo.NullText);
                return;
            }
            if (string.IsNullOrEmpty(this.Option))
            {
                //如果属性为空，输出消息对象的ToString()
                writer.Write(objMsg.ToString());
                return;
            }

            object val = GetValue(funcs, objMsg, Option);
            writer.Write(val == null ? "" : val.ToString());
        }

        #region 静态方法
        /// <summary>
        /// 循环方法列表，根据键值获取值
        /// </summary>
        /// <param name="func">方法列表委托</param>
        /// <param name="obj">对象</param>
        /// <param name="name">键值</param>
        /// <returns></returns>
        private static object GetValue(Func<object, string, object> func, object obj, string name)
        {
            object val = null;
            if (func != null)
            {
                foreach (Func<object, string, object> del in func.GetInvocationList())
                {
                    val = del(obj, name);
                    //如果获取的值不为null，则跳出循环
                    if (val != null)
                    {
                        break;
                    }
                }
            }
            return val;
        }

        /// <summary>
        /// 使用接口方式取值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>效率最高，避免了反射带来的效能损耗</remarks>
        private static object GetValueByInterface(object obj, string name)
        {
            object val = null;
            IGetObjectValueByKey objConverter = obj as IGetObjectValueByKey;
            if (objConverter != null)
            {
                val = objConverter.GetByKey(name);
            }
            return val;
        }

        /// <summary>
        /// 反射对象的获取属性，获取属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object GetValueByReflection(object obj, string name)
        {
            object val = null;
            Type t = obj.GetType();
            var propertyInfo = t.GetProperty(name);
            if (propertyInfo != null)
            {
                val = propertyInfo.GetValue(obj, null);
            }

            return val;
        }

        /// <summary>
        /// 反射对象的索引器，获取值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object GetValueByIndexer(object obj, string name)
        {
            object val = null;

            MethodInfo getValueMethod = obj.GetType().GetMethod("get_Item");
            if (getValueMethod != null)
            {
                val = getValueMethod.Invoke(obj, new object[] { name });
            }

            return val;
        }
        #endregion
    }
    public class ObjectPatternLayout : PatternLayout
    {
        public ObjectPatternLayout()
        {
            this.AddConverter("o", typeof(ObjectConverter));
        }
    }


    internal sealed class userIDPatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntity entity = loggingEvent.MessageObject as LogEntity;
            if (entity != null)
                writer.Write(entity.userID);
        }
    }
    internal sealed class userNamePatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntity entity = loggingEvent.MessageObject as LogEntity;
            if (entity != null)
                writer.Write(entity.userName);
        }
    }
    internal sealed class actionNamePatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntity entity = loggingEvent.MessageObject as LogEntity;
            if (entity != null)
                writer.Write(entity.actionName);
        }
    }
    internal sealed class controllerNamePatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntity entity = loggingEvent.MessageObject as LogEntity;
            if (entity != null)
                writer.Write(entity.controllerName);
        }
    }
    internal sealed class descriptionPatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntity entity = loggingEvent.MessageObject as LogEntity;
            if (entity != null)
                writer.Write(entity.description);
        }
    }
    internal sealed class messagePatternConverter : PatternLayoutConverter
    {
        override protected void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LogEntity entity = loggingEvent.MessageObject as LogEntity;
            if (entity != null)
                writer.Write(entity.errorMessage);
        }
    }

    public class MyPatternLayout : PatternLayout
    {
        public MyPatternLayout()
        {
            this.AddConverter("userID", typeof(userIDPatternConverter));
            this.AddConverter("userName", typeof(userNamePatternConverter));
            this.AddConverter("actionName", typeof(actionNamePatternConverter));
            this.AddConverter("controllerName", typeof(controllerNamePatternConverter));
            this.AddConverter("description", typeof(descriptionPatternConverter));
            this.AddConverter("message", typeof(messagePatternConverter));
        }
    }
}