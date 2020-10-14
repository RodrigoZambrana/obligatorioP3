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

            RepositorioConfiguraciones repoConfig = new RepositorioConfiguraciones();
            List<Cuota_Tasa> todasLasCuotasYTasas = repoConfig.CuotasyTasas();
            ViewBag.ListCuotas = todasLasCuotasYTasas;



            return View();
        }

        // POST: Proyecto/Create
        [HttpPost]
        public ActionResult Index(string titulo, string descripcion, decimal monto,int cantidadCuotas, string tipoProyecto, int? cantidadIntegrantes, string experiencia, HttpPostedFileBase imagen)
        {
            RepositorioProyectos repoProyectos = new RepositorioProyectos();          
            try
            {
                string usu = (string)Session["usuario"];
                RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                Solicitante u = (Solicitante)repoUsuarios.FindById(usu);
                RepositorioConfiguraciones repoConfig = new RepositorioConfiguraciones();
                Cuota_Tasa tasaycuotas = repoConfig.FindTasaYcuoutas(cantidadCuotas);

                if (tipoProyecto == "Cooperativo")
                {
                    Cooperativo c = new Cooperativo();
                    if (c.SubirArchivoGuardarNombre(imagen)) {
                        c.titulo = titulo;
                        c.descripcion = descripcion;
                        c.monto = monto;
                        c.cuotas = cantidadCuotas;
                        c.tasaInteres= tasaycuotas.tasa;
                        c.cantIntegrantes = (int)cantidadIntegrantes;
                        c.solicitante = u;

                           // Session["proyecto"] = c;
                       return RedirectToAction("Confirmar", c);           

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
                        c.cuotas = tasaycuotas.cuotas;
                        c.tasaInteres = tasaycuotas.tasa;
                        c.experiencia = experiencia;
                        c.solicitante = u;                        
                        return  RedirectToAction("Confirmar", c);                     
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
           //Proyecto p=(Proyecto) Session["proyecto"] ;
            if (p.GetType().Name.ToUpper()=="COOPERATIVO") {
                Cooperativo s=(Cooperativo)p;
                return RedirectToAction("Guardar", s);
            }

                Personal pe = (Personal)p;          
                return RedirectToAction("Guardar",pe);
        }

        [HttpPost]
        public ActionResult Guardar(Proyecto p)
        {
            try
            {
                RepositorioProyectos repoProyectos = new RepositorioProyectos();

                //Proyecto p = (Proyecto)Session["proyecto"];
                string usu = (string)Session["usuario"];
                RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                Solicitante u = (Solicitante)repoUsuarios.FindById(usu);
                p.solicitante = u;
                bool agregado = repoProyectos.Add(p);
                if (agregado)
                {
                    Session["proyecto"] = null;
                    return RedirectToAction("Index", "Solicitante");
                }
                Session["proyecto"] = null;
                return RedirectToAction("Index", "Proyecto");
            }
            catch
            {
                RepositorioConfiguraciones repoConfig = new RepositorioConfiguraciones();
                List<Cuota_Tasa> todasLasCuotasYTasas = repoConfig.CuotasyTasas();
                ViewBag.ListCuotas = todasLasCuotasYTasas;
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

        // GET: Pryecto/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

    }
}
