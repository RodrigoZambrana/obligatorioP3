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
            RepositorioProyectos repoProyectos = new RepositorioProyectos();

            if (repoProyectos.findPendiente())
            {
                 ViewBag.Mensaje = "Existe un proyecto pendiente, no se puede agregar otro";
                return View();

            }

            List<int> cuotas = repoProyectos.CargarCuotas();

            return View(cuotas);
        }

        // POST: Proyecto/Create
        [HttpPost]
        public ActionResult Index(string titulo, string descripcion, decimal monto,int cantidadCuotas, string tipoProyecto, int? cantidadIntegrantes, string experiencia,string imagen)
        {
            RepositorioProyectos repoProyectos = new RepositorioProyectos();
           
            try
            {
                RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                string usu = (string)Session["usuario"];
                Solicitante u = (Solicitante)repoUsuarios.FindById(usu);
                Proyecto p = new Proyecto();

                if (tipoProyecto == "Cooperativo")
                {
                    p = new Cooperativo
                    {
                        titulo = titulo,
                        descripcion = descripcion,
                        monto = monto,
                        cuotas = cantidadCuotas,
                        cantIntegrantes = (int)cantidadIntegrantes,
                        rutaImagen = imagen,
                        solicitante = u,

                    };
                }

                if (tipoProyecto == "Personal")
                {
                    p = new Personal
                    {
                        titulo = titulo,
                        descripcion = descripcion,
                        monto = monto,
                        cuotas = cantidadCuotas,
                        experiencia = experiencia,
                        rutaImagen = imagen,
                        solicitante = u

                    };
                }

                if (repoProyectos.Add(p))
                {

                    return View("Confirmar", p);
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
        //public ActionResult Confirmar()
        //{
        //    return View();
        //}

        // POST: Proyecto/Delete/5
        [HttpPost]
        public ActionResult Confirmar(Proyecto p)
        {

            try
            {
                RepositorioProyectos repoProyectos = new RepositorioProyectos();
                bool agregado = repoProyectos.Add(p);
                if (agregado)
                {

                    return View("Guardar", p);
                }
                return View(p);
            }
            catch
            {
                return View();
            }
        }


        // GET: Proyecto/Delete/5
        //public ActionResult Confirmar()
        //{
        //    return View();
        //}

        // POST: Proyecto/Delete/5
        [HttpPost]
        public ActionResult Guardar(Proyecto p)
        {
            try
            {


                return RedirectToAction("Index", "Solicitante");
            }
            catch
            {
                return View();
            }
        }


    }
}
