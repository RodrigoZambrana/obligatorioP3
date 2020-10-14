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
            string usu = (string)Session["usuario"];
            if (repoProyectos.findPendiente(usu))
            {
                 ViewBag.Mensaje = "Existe un proyecto pendiente, no se puede agregar otro";
                return View();

            }

            List<int> cuotas = repoProyectos.CargarCuotas();

            return View(cuotas);
        }

        // POST: Proyecto/Create
        [HttpPost]
        public ActionResult Index(string titulo, string descripcion, decimal monto,int cantidadCuotas, string tipoProyecto, int? cantidadIntegrantes, string experiencia, HttpPostedFileBase imagen)
        {
            RepositorioProyectos repoProyectos = new RepositorioProyectos();          
            try
            {
                RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                string usu = (string)Session["usuario"];
                Solicitante u = (Solicitante)repoUsuarios.FindById(usu);

                if (tipoProyecto == "Cooperativo")
                {
                    Cooperativo c = new Cooperativo();
                    if (c.SubirArchivoGuardarNombre(imagen)) {
                        c.titulo = titulo;
                        c.descripcion = descripcion;
                        c.monto = monto;
                        c.cuotas = cantidadCuotas;
                        c.cantIntegrantes = (int)cantidadIntegrantes;
                        c.solicitante = u;         
                }
                    if (c != null)
                    {
                        Session["proyecto"] = c;
                        return View("Confirmar", c);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                if (tipoProyecto == "Personal")
                {
                    Personal c = new Personal();
                    if (c.SubirArchivoGuardarNombre(imagen))
                    {
                        c.titulo = titulo;
                        c.descripcion = descripcion;
                        c.monto = monto;
                        c.cuotas = cantidadCuotas;
                        c.experiencia = experiencia;
                        c.solicitante = u;
                    }
                    if (c != null)
                    {
                        Session["proyecto"] = c;
                        return View("Confirmar", c);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");

            }
            catch
            {
                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public ActionResult Confirmar(Proyecto p)
        {
            
            return RedirectToAction("Guardar");
        }

        [HttpPost]
        public ActionResult Guardar()
        {
            try
            {
                RepositorioProyectos repoProyectos = new RepositorioProyectos();
                Proyecto p = (Proyecto)Session["proyecto"];
                bool agregado = repoProyectos.Add(p);
                if (agregado)
                {
                    Session["proyecto"] = null;
                    return RedirectToAction("Index", "Solicitante");
                }
                Session["proyecto"] = null;
                return RedirectToAction("Index", "Solicitante");
            }
            catch
            {
                return View("Index");
            }
        }

        public ActionResult Cancelar()
        {
            Session["proyecto"] = null;
            return View("index");
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
        public ActionResult Edit(Proyecto p, string comentarios)
        {
            try
            {
                RepositorioProyectos repo = new RepositorioProyectos();
                Proyecto proy = repo.FindById(p.id);
                p.solicitante = proy.solicitante;
                bool actualizado= repo.Update(p, comentarios, (string)Session["usuario"]);
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
        
    }
}
