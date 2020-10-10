﻿using Dominio;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["usuario"] == null || (String)Session["rol"] != "ADMIN")
            {
                Session["usuario"] = null;

                Session["rol"] = null;

                return RedirectToAction("Index", "Home");
            }

            string usuario = (string)Session["usuario"];

            RepositorioProyectos repoUsuarios = new RepositorioProyectos();
            IEnumerable<Proyecto> todosLosProyectos =repoUsuarios.FindAll();

            return View (todosLosProyectos);
        }

    }


}
