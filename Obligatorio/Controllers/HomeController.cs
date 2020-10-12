using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                Session["edad"] = u.Edad();

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
            Usuario existeUsuario = repoUsuarios.FindById(solicitante.cedula);
            if (existeUsuario == null)
            {
                try
                {
                    bool validarCi = validarCedula(solicitante.cedula);
                    bool validarCel = validarCelular(solicitante.celular);
                    bool validarCorreo = validarEmail(solicitante.email);
                    bool validarPass = validarPassword(solicitante.password);
                    if (validarCi)
                    {
                        if (validarCel)
                        {
                            if (validarCorreo)
                            {
                                if (validarPass)
                                {
                                    bool agregado = repoUsuarios.Add(solicitante);
                                    if (agregado)
                                    {

                                        return RedirectToAction("Index", "Solicitante", solicitante);
                                    }
                                    else
                                    {
                                        return RedirectToAction("Create");
                                    }
                                }
                                else
                                {
                                    ViewBag.Mensaje = "Contraseña no válida";
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.Mensaje = "Correo no válido";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Mensaje = "Celular no válido";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Cédula incorrecta";
                        return View();
                    }
                }
                catch
                {
                    return View();
                }
            }
            ViewBag.Mensaje = "El usuario ya existe";
            return View();
        }

            private bool validarCedula(string ci)
            {
                bool valido = false;
                if (ci.Length != 8)
                {
                    return valido;
                }
                foreach (char c in ci)
                {
                    if (!Char.IsNumber(c))
                    {
                        return valido;
                    }
                }
                return true;
            }

            private bool validarPassword(string pass)
            {
                bool valido = false;
                int num = 0;
                int may = 0;
                int min = 0;
                if (pass.Length < 5)
                {
                    return valido;
                }
                foreach (char c in pass)
                {
                    if (Char.IsNumber(c))
                    {
                        num = num + 1;
                    }
                    if (Char.IsUpper(c))
                    {
                        may = may + 1;
                    }
                    else
                    {
                        min = min + 1;
                    }
                }
                if (min == 0 || may == 0 || num == 0)
                {
                    return valido;
                }
                return true;
            }

            private bool validarCelular(string cel)
            {
                bool valido = false;
                if (cel.Length != 9)
                {
                    return valido;
                }
                foreach (char c in cel)
                {
                    if (!Char.IsNumber(c))
                    {
                        return valido;
                    }
                }
                if (cel[0].ToString() != "0")
                {
                    return valido;
                }
                if (cel[1].ToString() != "9")
                {
                    return valido;
                }
                if (cel[2].ToString() == "0")
                {
                    return valido;
                }
                return true;
            }
            private bool validarEmail(string email)
            {
                Regex re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
                RegexOptions.IgnoreCase);
                return re.IsMatch(email);
            }
        


    }
}
