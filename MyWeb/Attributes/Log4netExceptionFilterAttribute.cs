using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;

namespace MyWeb
{
    /// <summary>
    /// 异常过滤器，通过log4net记录日志
    /// </summary>
    public class Log4netExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            LogHelper log = new LogHelper();
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            string info = string.Format("【Controller:{0}】【Action:{1}】",controller,action);

            if (filterContext.Exception.InnerException!=null)
            {
                log.Debug(info, filterContext.Exception.InnerException);
            }
            else
            {
                log.Debug(info, filterContext.Exception);
            }
            
            //页面跳转到错误页面
            filterContext.HttpContext.Response.Redirect("~/Error.html");
        }
    }
}