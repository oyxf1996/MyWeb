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

            string controller = filterContext.RequestContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RequestContext.RouteData.Values["action"].ToString();
            string info = string.Format("【*】Controller:{0}，Action:{1}【*】", controller,action);

            //将异常入队，交由另一个线程处理
            if (filterContext.Exception.InnerException != null)
            {
                ExceptionHelper.Enqueue(info, filterContext.Exception.InnerException);
            }
            else
            {
                ExceptionHelper.Enqueue(info,filterContext.Exception);
            }

            //页面跳转到错误页面
            filterContext.HttpContext.Response.Redirect("~/Error.html");
        }
    }
}