using Dominio;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["usuario"] == null || (String)Session["rol"] != "ADMIN")
            {
                Session["usuario"] = null;

                Session["rol"] = null;

                return RedirectToAction("Index", "Home");
            }

            string usuario = (string)Session["usuario"];

            RepositorioProyectos repoUsuarios = new RepositorioProyectos();
            IEnumerable<Proyecto> todosLosProyectos =repoUsuarios.FindAll();

            return View (todosLosProyectos);
        }

        //GetFiltrar
        public ActionResult Filtrar()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Filtrar(string cedula,DateTime fecha, string estado, string texto)
        {
            

            

            RepositorioProyectos repoProyectos = new RepositorioProyectos();
            IEnumerable<Proyecto> ProyectosFiltrados = repoProyectos.Filtrar(cedula,fecha,estado,texto);

            return View(ProyectosFiltrados);
        }


    }



}



