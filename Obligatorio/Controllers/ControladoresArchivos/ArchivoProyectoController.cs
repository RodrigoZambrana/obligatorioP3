using Dominio;
using Repositorios;
using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Obligatorio.Controllers.ControladoresArchivos
{
    public class ArchivoProyectoController : Controller
    {
        private static string ArchivoProyectos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Proyectos.txt");
        private const int CANT_ATRIBUTOS_Proyecto = 8;

        public ActionResult Index()
        {
            try
            {
                RepositorioProyectos repo = new RepositorioProyectos();
                IEnumerable<Proyecto> todosLosProyectos = repo.FindAll();
                //El constructor de StreamWriter recibe el nombre del archivo y true si se desean
                //agregar registros, false si se va a sobrescribir el archivo.

                using (StreamWriter sw = new StreamWriter(ArchivoProyectos, false))
                {
                    foreach (Proyecto unProyecto in todosLosProyectos)
                    {
                        sw.WriteLine(unProyecto.titulo + "|" + unProyecto.descripcion + "|" + unProyecto.monto + "|" + unProyecto.cuotas + "|" + unProyecto.estado + "|" +
                            unProyecto.fechaPresentacion.Date + "|" + unProyecto.puntaje + "|" + unProyecto.tasaInteres);
                    }
                }
                ViewBag.LaRuta = ArchivoProyectos;
                return View();
            }
            catch (FileNotFoundException) { throw; }
            catch (PathTooLongException) { throw; }
            catch (InvalidDataException) { throw; }
            catch (DirectoryNotFoundException) { throw; }
            catch (DriveNotFoundException) { throw; }
            catch (Exception) { throw; }
        }

        public static List<Proyecto> ObtenerTodos()
        {
            List<Proyecto> retorno = new List<Proyecto>();
            using (StreamReader sr = System.IO.File.OpenText(ArchivoProyectos))
            {
                string linea = sr.ReadLine();
                while ((linea != null))
                {
                    if (linea.IndexOf("|") > 0)
                    {
                        retorno.Add(ObtenerDesdeString(linea, "|"));
                    }
                    linea = sr.ReadLine();
                }
            }
            return retorno;
        }

        private static Proyecto ObtenerDesdeString(string dato, string delimitador)
        {
            string[] vecDatos = dato.Split(delimitador.ToCharArray());
            if (vecDatos.Length == CANT_ATRIBUTOS_Proyecto) //Verificar que la línea está ok
            {
                return new Proyecto
                {
                    titulo = vecDatos[0],
                    descripcion = vecDatos[1],
                    monto = Decimal.Parse(vecDatos[2]),
                    cuotas=int.Parse(vecDatos[3]),
                    estado = vecDatos[4],
                    fechaPresentacion = DateTime.Parse(vecDatos[5]),
                    puntaje = int.Parse(vecDatos[6]),
                    tasaInteres = Decimal.Parse(vecDatos[7])

                };
            }
            else
                return null;
        }
    }
}
