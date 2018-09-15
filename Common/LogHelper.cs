using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Common
{
    public class LogHelper
    {
        private static log4net.ILog log = null;
        public LogHelper()
        {
            //当logger名称不存在在配置文件中，默认是取root的属性
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public LogHelper(string loggerName) : this()
        {
            log = log4net.LogManager.GetLogger(loggerName);
        }


        public void Debug(string debug)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(debug);
            }
        }
        public void Debug(string debug, Exception ex)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(debug, ex);
            }
        }

        public void Info(string info)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(info);
            }
        }
        public void Info(string info,Exception ex)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(info, ex);
            }
        }
        public void Info(string info, Exception ex, string fullname)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(info + " 【方法名：】" + fullname + "\n", ex);
            }
        }
        public void Warn(string warn)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(warn);
            }
        }
        public void Warn(string warn, Exception ex)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(warn, ex);
            }
        }
        public void Error(string error)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(error);
            }
        }
        public void Error(string error, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(error, ex);
            }
        }

        public void Fatal(string fatal)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(fatal);
            }
        }
        public void Fatal(string fatal, Exception ex)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(fatal, ex);
            }
        }

    }
}
