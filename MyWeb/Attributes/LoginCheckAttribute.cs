using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWeb
{
    public class LoginCheckAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action方法执行后，View显示前【比OnResultExecuting先执行】
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// Action方法执行前【登陆验证和权限判断】
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            filterContext.HttpContext.Response.Write("OnActionExecuting方法");

            //打了SkipLoginCheckAttribute特性标签不做登陆验证
            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipLoginCheckAttribute),false))
            {
                return;
            }

            //登陆验证、权限验证
            if (filterContext.HttpContext.Session["CurrentUser"] == null)
            {
                filterContext.Result = new RedirectResult("~/Deault/Login");
                return;
            }
        }

        /// <summary>
        /// 返回结果后，View显示后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        /// Action方法执行后，View显示前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}