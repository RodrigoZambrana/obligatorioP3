using Dominio;
using Ejemplo_View_Model_2.Models;
using Obligatorio.Models;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio.Controllers
{
    public class ProyectoModelController : Controller
    {
        // GET: ProyectoModel
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProyectoModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProyectoModel/Create
        public ActionResult Create()
        {
            return View(new ProyectoModel());
        }

        // POST: ProyectoModel/Create

        [HttpPost]
        public ActionResult Create(ProyectoModel p)
        {
            try
            {
                string usu = (string)Session["usuario"];
                RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                Solicitante u = (Solicitante)repoUsuarios.FindById(usu);
                RepositorioConfiguraciones repoConfig = new RepositorioConfiguraciones();
                //Cuota_Tasa tasaycuotas = repoConfig.FindTasaYcuoutas(cantidadCuotas);

                return RedirectToAction("Confirmar", p);

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Confirmar(ProyectoModel p)
        {
            return RedirectToAction("Guardar", p);

        }

        [HttpPost]
        public ActionResult Guardar(ProyectoModel p)
        {
            return RedirectToAction("Index", "Solicitante");

        }






        // GET: ProyectoModel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProyectoModel/Edit/5
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

        // GET: ProyectoModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProyectoModel/Delete/5
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
