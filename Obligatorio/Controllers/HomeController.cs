using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrarse()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Home()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}