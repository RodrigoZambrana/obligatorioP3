using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio.Controllers
{
    public class SolicitanteController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

        public ActionResult PresentarProyecto()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
