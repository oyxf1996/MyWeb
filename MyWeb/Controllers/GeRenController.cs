using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
{
    public class GeRenController : Controller
    {
        // GET: GeRen
        public ActionResult Intro()
        {
            return View();
        }
        public ActionResult Program()
        {
            return View();
        }

        public ActionResult Life()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    filterContext.ExceptionHandled = true;

        //    //Log the error!!

        //    //Redirect or return a view, but not both.
        //    filterContext.Result = RedirectToAction("Index", "ErrorHandler");
        //    // OR 
        //    filterContext.Result = new ViewResult
        //    {
        //        ViewName = "~/Views/ErrorHandler/Index.cshtml"
        //    };
        //}
    }
}