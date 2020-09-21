using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Repositorios;

namespace Obligatorio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string cedula, string password)
        {
            RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
            Usuario u = repoUsuarios.FindById(cedula);
            if (u != null && u.password.Equals(password))
            {

                Session["usuario"] = u.cedula;
                Session["rol"] = u.GetType().Name.ToUpper();

                 if (u is Admin)
                {
                    Admin a= (Admin) u;
                    return RedirectToAction("Index", "Admin", a);
                }
                if (u is Solicitante)
                {
                    Solicitante s = (Solicitante)u;
                    return RedirectToAction("Index", "Solicitante",s);
                }

            }
            else
            {
                ViewBag.mensaje = "Login erroneo";
            }

            return View();
        }


        public ActionResult Salir()
        {
            Session["usuario"] = null;
            Session["role"] = null;

            return View("index");

        }




        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Solicitante solicitante)
        {
            RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
            try
            {
                bool agregado = repoUsuarios.Add(solicitante);
                if (agregado)
                {

                    return RedirectToAction("Index", "Solicitante", solicitante);
                }
                else {
                    return RedirectToAction("Create");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
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
