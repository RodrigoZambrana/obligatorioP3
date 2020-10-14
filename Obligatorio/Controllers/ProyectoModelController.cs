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
                Session["proyecto"] = p;
                return RedirectToAction("Confirmar",p);

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Confirmar(ProyectoModel p)
        {
            //string usu = (string)Session["usuario"];
            //RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
            //Solicitante u = (Solicitante)repoUsuarios.FindById(usu);
            ////Cuota_Tasa tasaycuotas = repoConfig.FindTasaYcuoutas(cantidadCuotas);
            //ProyectoModel pro =(ProyectoModel)Session["proyecto"];
            //if (pro.tipo == "Cooperativo")
            //{
            //    Cooperativo c = new Cooperativo();
            //    if (c.SubirArchivoGuardarNombre(pro.Archivo))
            //    {
            //        c.titulo = pro.titulo;
            //        c.descripcion = pro.descripcion;
            //        c.monto = pro.monto;
            //        c.cuotas = pro.cuotas;
            //        c.cantIntegrantes = pro.cantidadIntegrantes;
            //        c.solicitante = u;

            //        // Session["proyecto"] = c;
            //        return RedirectToAction("Guardar", c);

            //    }
            //}
            //if (pro.tipo == "Personal")
            //{
            //    Personal c = new Personal();
            //    if (c.SubirArchivoGuardarNombre(pro.Archivo))
            //    {
            //        c.titulo = pro.titulo;
            //        c.descripcion = pro.descripcion;
            //        c.monto = pro.monto;
            //        c.cuotas = pro.cuotas;
            //        c.experiencia = pro.experiencia;
            //        c.solicitante = u;
            //        return RedirectToAction("Guardar", c);
            //    }

            //}
            return RedirectToAction("Guardar");

        }

        [HttpPost]
        public ActionResult Guardar(ProyectoModel p)
        {
            string usu = (string)Session["usuario"];
            RepositorioUsuarios repoUsuarios = new RepositorioUsuarios();
            Solicitante u = (Solicitante)repoUsuarios.FindById(usu);
            //Cuota_Tasa tasaycuotas = repoConfig.FindTasaYcuoutas(cantidadCuotas);
            ProyectoModel pro = (ProyectoModel)Session["proyecto"];
            if (pro.tipo == "Cooperativo")
            {
                Cooperativo c = new Cooperativo();
                if (c.SubirArchivoGuardarNombre(pro.Archivo))
                {
                    c.titulo = pro.titulo;
                    c.descripcion = pro.descripcion;
                    c.monto = pro.monto;
                    c.cuotas = pro.cuotas;
                    c.cantIntegrantes = pro.cantidadIntegrantes;
                    c.solicitante = u;

                    RepositorioProyectos repo = new RepositorioProyectos();

                    repo.Add(c);
                    return RedirectToAction("Solicitante", "Index");

                }
            }
            if (pro.tipo == "Personal")
            {
                Personal c = new Personal();
                if (c.SubirArchivoGuardarNombre(pro.Archivo))
                {
                    c.titulo = pro.titulo;
                    c.descripcion = pro.descripcion;
                    c.monto = pro.monto;
                    c.cuotas = pro.cuotas;
                    c.experiencia = pro.experiencia;
                    c.solicitante = u;
                    RepositorioProyectos repo = new RepositorioProyectos();

                    repo.Add(c);
                    return RedirectToAction("Solicitante", "Index");
                }

            }

            return View("Create", new ProyectoModel());
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
