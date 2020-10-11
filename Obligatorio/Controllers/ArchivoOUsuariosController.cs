using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio.Controllers
{
    public class ArchivoOUsuariosController : Controller
    {
        // GET: Archivo

        private string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xyz.txt");

        public ActionResult Index()

        {

            try

            {

                //	using (StreamWriter sw = new StreamWriter(ruta))

                //	{

                //		sw.Write("Hola estoy grabando el archivo");

                //		sw.WriteLine("Y sigo por acá....");

                //		sw.Write("Y acá termina");

                //		ViewBag.LaRuta = ruta;

                //		return View();

                //	}

                //}



                //string texto = "";

                //string lineaLeida;

                //using (StreamReader sr = new StreamReader(ruta))

                //{

                //	lineaLeida = sr.ReadLine();

                //	while (lineaLeida != null)

                //	{

                //		texto += lineaLeida;

                //		lineaLeida = sr.ReadLine();

                //	}

                //	ViewBag.Texto = texto;

                //	return View();

                //}



                if (!System.IO.File.Exists(ruta))

                {

                    ViewBag.Texto = "No existe el archivo";

                    return View();

                }

                StringBuilder builder = new StringBuilder();

                string lineaLeida;

                using (StreamReader sr = new StreamReader(ruta))

                {

                    lineaLeida = sr.ReadLine();

                    while (lineaLeida != null)

                    {

                        builder.AppendLine(lineaLeida);

                        lineaLeida = sr.ReadLine();

                    }

                    ViewBag.Texto = builder.ToString();

                    return View();

                }

                ViewBag.LaRuta = ruta;

                return View();

            }







            catch (IOException ex)

            {



                throw;

            }



        }
    }
}
