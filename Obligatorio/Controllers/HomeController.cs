using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace Obligatorio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()

        {
            //Declarar la cadena de conexión e instanciar el objeto para conectarse a la BD

            string cadenaConexion = ConfigurationManager
                                    .ConnectionStrings["miConexion"]
                                    .ConnectionString;
            SqlConnection cn = new SqlConnection();

            //Setearle la cadena de conexión

            cn.ConnectionString = cadenaConexion;

            //cn.Open(); esto es necesario solo como modo de prueba inicio para saber si se conecta a la base

            //cn.Close();
            //	Si armo la cadena de comando utilizando concatenación o intercalación

            //	abre la puerta a SQL INJECTION :-(

            //	string cadenaComando = $"INSERT INTO Persona VALUES ('{nombrePersona}', '{apePersona}')";

            //	Instanciar el comando y prepararlo
           
            //string cadenacomando = "INSERT INTO AdminPrueba VALUES (@cedula, @password)";
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = cadenacomando;
            //cmd.Connection = cn;
            //int cedula = 123458;
            //string password = "password";
            //cmd.Parameters.Add(new SqlParameter("@cedula", cedula));
            //cmd.Parameters.Add(new SqlParameter("@password", password));

            //Abro la conexión
            cn.Open();

            //Trabajar con el comando (actualizar, obtener un reader, etc).

            //int filas = cmd.ExecuteNonQuery();

            //Cerrar la conexión para dejarla disponible para otros usuarios.

            cn.Close();
        //    if (filas > 0)

        //   {
        //       Console.WriteLine("Insertado!");

        //    }
        //    else { Console.WriteLine("Error"); }
           return View();
        }



        public ActionResult Registrarse()

        {

            //ViewBag.Message = "Your application description page.";



            return View();

        }

    }
}