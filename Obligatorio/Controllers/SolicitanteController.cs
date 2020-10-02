using Dominio;
using Repositorios;
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
            if (Session["usuario"] == null || (string)Session["rol"] != "SOLICITANTE")
            {
                Session["usuario"] = null;

                Session["rol"] = null;

                return RedirectToAction("Index", "Home");
            }

            string usuario = (string)Session["usuario"];
            return View ();
        }

        public ActionResult PresentarProyecto()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProyectosDeUsuario()
        {
            RepositorioProyectos repoProyectos = new RepositorioProyectos();
            IEnumerable<Proyecto> listProyectos = null;

            //ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}
