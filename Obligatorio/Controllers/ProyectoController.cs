using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        // POST: Proyecto/Create
        [HttpPost]
        public ActionResult Cooperativo(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proyecto/Cooperativo
        public ActionResult Personal()
        {
            return View();
        }

        // POST: Proyecto/Create
        [HttpPost]
        public ActionResult Personal(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
