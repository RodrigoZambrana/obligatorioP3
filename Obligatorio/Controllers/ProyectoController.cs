using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Repositorios;

namespace Obligatorio.Controllers
{
    public class ProyectoController : Controller
    {
        // GET: Proyecto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Proyecto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Proyecto/Cooperativo
        public ActionResult Cooperativo()
        {
            RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
            string cedula = (string)Session["usuario"];
            Usuario u = repoUsuarios.FindById(cedula);
           

                ViewBag.mensaje = "El empleado se ha borrado";

            
            return View();
        }

        // POST: Proyecto/Create
        [HttpPost]
        public ActionResult Cooperativo(Cooperativo pCooperativo)
        {
            RepositorioProyectos repoProyectos = new RepositorioProyectos();
           
            try
            {
                RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                string usu= (string) Session["usuario"];
                Solicitante u = (Solicitante)repoUsuarios.FindById(usu);
                pCooperativo.setSolicitante(u);
                bool agregado = repoProyectos.Add(pCooperativo);
                if (agregado)
                {

                    return RedirectToAction("Index", "Solicitante");
                }
                else
                {
                    return RedirectToAction("Cooperativo");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Proyecto");

            }
        }

        // GET: Proyecto/Personal
        public ActionResult Personal()
        {
            return View();
        }

        // POST: Proyecto/Create
        [HttpPost]
        public ActionResult Personal(Personal pPersonal)
        {

            RepositorioProyectos repoProyectos = new RepositorioProyectos();
            try
            {
                RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                string usu = (string)Session["usuario"];
                Solicitante u = (Solicitante)repoUsuarios.FindById(usu);
                pPersonal.setSolicitante(u);
                bool agregado = repoProyectos.Add(pPersonal);
                if (agregado)
                {

                    return RedirectToAction("Index", "Solicitante");
                }
                else
                {
                    return RedirectToAction("Personal");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Proyecto");

            }
        }

        public ActionResult ListadoDeProyectos()
        {
            if (Session["usuario"] == null || (string)Session["rol"] == "SOLICITANTE")
            {
                Session["usuario"] = null;
                Session["role"] = null;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult ListadoDeProyectos(string proyectos, DateTime? fchFinalizado)
        {
            ViewBag.mensaje = " ";
            if (proyectos != "" && proyectos != null && fchFinalizado != null)

            {
               
                ViewBag.mensaje = "Debe seleccionar un proyecto";

            }


            List<Proyecto> prox = new List<Proyecto>();
            return View(prox);
        }





        // GET: Proyecto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proyecto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proyecto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proyecto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
