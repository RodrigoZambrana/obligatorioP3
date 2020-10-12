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
        private decimal montoMaximo = 20000;
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
                if (pCooperativo.cantIntegrantes < 10)
                {
                    decimal porcentaje = (decimal)0.02 * pCooperativo.cantIntegrantes;
                    pCooperativo.monto = pCooperativo.monto + (pCooperativo.monto * porcentaje);
                }
                else
                {
                    decimal porcentaje = (decimal)0.2;
                    pCooperativo.monto = pCooperativo.monto + (pCooperativo.monto * porcentaje);
                }
                if (pCooperativo.monto > montoMaximo * (decimal)1.20)
                {
                    ViewBag.Mensaje = "El monto máximo no puede superar un 20% mayor que el fijado por la empresa ";
                    return RedirectToAction("Index", "Proyecto");
                }
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
                if (pPersonal.monto - pPersonal.monto*(decimal)0.20> montoMaximo)
                {
                    ViewBag.Mensaje = "El monto máximo debe ser un 20% menor que el fijado por la empresa ";
                    return RedirectToAction("Index", "Proyecto");
                }
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


        // GET: Proyecto/Edit/
        public ActionResult Edit(int id)
        {
            RepositorioProyectos repositorioProyectos = new RepositorioProyectos();
            Proyecto p = repositorioProyectos.FindById(id);
                
            return View(p);
        }

        // POST: Proyecto/Edit/5
        [HttpPost]
        public ActionResult Edit(Proyecto p)
        {
            try
            {
                RepositorioProyectos repo = new RepositorioProyectos();
                bool actualizado= repo.Update(p);
                if (actualizado) {
                    return RedirectToAction("Index", "Admin");
                }
                return View(p);
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
