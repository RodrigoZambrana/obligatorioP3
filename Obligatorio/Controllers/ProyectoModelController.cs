using Dominio;
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
            if (Session["usuario"] == null || (string)Session["rol"] != "SOLICITANTE")
            {
                Session["usuario"] = null;

                Session["rol"] = null;

                return RedirectToAction("Index", "Home");
            }
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

        // GET: ProyectoModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProyectoModel/Create
        public ActionResult Create()
        {
            if (Session["usuario"] == null || (string)Session["rol"] != "SOLICITANTE")
            {
                Session["usuario"] = null;

                Session["rol"] = null;

                return RedirectToAction("Index", "Home");
            }
            RepositorioProyectos repoProyectos = new RepositorioProyectos();
            string usu = (string)Session["usuario"];
            if (repoProyectos.findPendiente(usu))
            {
                ViewBag.Mensaje = "Existe un proyecto pendiente, no se puede agregar otro";
                return View(new ProyectoModel());

            }

            RepositorioConfiguraciones repoConfig = new RepositorioConfiguraciones();
            List<Cuota_Tasa> todasLasCuotasYTasas = repoConfig.CuotasyTasas();       
            return View(new ProyectoModel());
        }

        // POST: ProyectoModel/Create
        [HttpPost]
        public ActionResult Create(ProyectoModel pro)
        {
            try
            {
                string usu = (string)Session["usuario"];
                RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
                Solicitante u = (Solicitante)repoUsuarios.FindById(usu);

                if (ModelState.IsValid && pro.SubirArchivoGuardarNombre())
                {
             
                    if (pro.tipo == "Cooperativo")
                    {
                        Cooperativo c = new Cooperativo();
                        if (pro.SubirArchivoGuardarNombre())
                        {
                            c.titulo = pro.titulo;
                            c.descripcion = pro.descripcion;
                            c.monto = pro.monto;
                            c.cuotas = pro.cuotas;
                            c.cantIntegrantes = pro.cantidadIntegrantes;
                            c.solicitante = u;
                            RepositorioProyectos repo = new RepositorioProyectos();
                            if (repo.Add(c))
                            {
                                return RedirectToAction("Index","Solicitante");
                            }
                            else
                            {
                                ViewBag.Mensaje = "Error al agregar un proyecto. Reintente";
                                return View(pro);
                            }
                        }
                    }
                    if (pro.tipo == "Personal")
                    {
                        Personal c = new Personal();
                        if (pro.SubirArchivoGuardarNombre())
                        {
                            c.titulo = pro.titulo;
                            c.descripcion = pro.descripcion;
                            c.monto = pro.monto;
                            c.cuotas = pro.cuotas;
                            c.experiencia = pro.experiencia;
                            c.solicitante = u;
                            RepositorioProyectos repo = new RepositorioProyectos();
                            repo.Add(c);
                            return RedirectToAction("Index", "Solicitante");
                        }

                    }
                }
                ViewBag.Mensaje = "Error al agregar un proyecto. Reintente";
                return View(pro);
            }
            catch
            {
                return View(pro);
            }
        }    
    }


}
