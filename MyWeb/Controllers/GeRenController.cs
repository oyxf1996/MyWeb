using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}