using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Repositorios
{
    public class RepositorioProyectos : IRepositorio<Proyecto>
    {           
        public bool Add(Proyecto unProyecto)
        {
            //Tendría todo el código de ADO.NET para hacer el INSERT  a través de comandos.

            if (unProyecto == null || !unProyecto.Validar())
                return false;
            Conexion unaCon = new Conexion();
            SqlConnection cn = unaCon.CrearConexion();
            SqlCommand cmd = new SqlCommand("INSERT INTO Proyectos VALUES (@Cedula,@Titulo,@Descripcion,@Monto,@Cuotas,@NombreImagen,@Estado,@FechaPresentacion,@Puntaje,@TasaInteres,@Tipo);SELECT CAST(SCOPE_IDENTITY() AS INT)", cn);
            cmd.Parameters.Add(new SqlParameter("@Cedula", unProyecto.solicitante.cedula));
            cmd.Parameters.Add(new SqlParameter("@Titulo", unProyecto.titulo));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", unProyecto.descripcion));
            cmd.Parameters.Add(new SqlParameter("@Monto", unProyecto.monto));
            cmd.Parameters.Add(new SqlParameter("@Cuotas", unProyecto.cuotas));
            cmd.Parameters.Add(new SqlParameter("@NombreImagen", unProyecto.rutaImagen));
            cmd.Parameters.Add(new SqlParameter("@Estado", unProyecto.estado));
            cmd.Parameters.Add(new SqlParameter("@fechaPresentacion", unProyecto.fechaPresentacion));
            cmd.Parameters.Add(new SqlParameter("@Puntaje", unProyecto.puntaje));
            cmd.Parameters.Add(new SqlParameter("@TasaInteres", unProyecto.tasaInteres));
            cmd.Parameters.Add(new SqlParameter("@Tipo", unProyecto.GetType().Name.ToUpper()));

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            SqlTransaction trn = null;
            try
            {
                if (unaCon.AbrirConexion(cn))
                {
                    //Inicia la transacción
                    trn = cn.BeginTransaction();
                    //Asignarle la transacción a cada uno de los comandos
                    //que querés que sean transaccionales:
                    cmd.Transaction = trn;
                    // cmd.ExecuteNonQuery();
                    int idAsignado = (int)cmd.ExecuteScalar();
                    if (unProyecto is Personal)
                    {
                        Personal per = (Personal)unProyecto;
                        cmd2.CommandText = "INSERT INTO Personales VALUES (@Id,@Experiencia)";
                        cmd2.Parameters.Add(new SqlParameter("@Id", idAsignado));
                        cmd2.Parameters.Add(new SqlParameter("@Experiencia", per.experiencia));
                         

                    }
                    else if (unProyecto is Cooperativo)
                    {
                        Cooperativo coo = (Cooperativo)unProyecto;
                        cmd2.CommandText = "INSERT INTO Cooperativos VALUES (@Id,@cantIntegrantes)";
                        cmd2.Parameters.Add(new SqlParameter("@Id", idAsignado));
                        cmd2.Parameters.Add(new SqlParameter("@cantIntegrantes", coo.cantIntegrantes));
                        

                    }
                    else
                    {
                        return false;
                    }
                    //Asignarle la transacción al segundo comando
                    cmd2.Transaction = trn;

                    cmd2.ExecuteNonQuery();
                    //Si llegué ácá se pudieron ejecutar todas las operaciones
                    //xq sino hubiera saltado x el catch, así que hacemos el commit:
                    trn.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                unaCon.CerrarConexion(cn);
            }
        }

        public bool findPendiente(string cedula)
        {
            Conexion unaCon = new Conexion();

            SqlConnection cn = unaCon.CrearConexion();

            SqlCommand cmd = new SqlCommand(@"SELECT P.*
                 FROM Proyectos P
                 WHERE P.Estado='PENDIENTE' AND P.Cedula=@Cedula;", cn);

              cmd.Parameters.AddWithValue("@Cedula", cedula);
            try
            {
                if (unaCon.AbrirConexion(cn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            catch (SqlException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                unaCon.CerrarConexion(cn);
            }
        }

        public IEnumerable<Proyecto> FindAll()
        {
            Conexion unaCon = new Conexion();
            SqlConnection cn = new Conexion().CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT P.Id,P.Cedula,P.Titulo,P.Descripcion,P.Monto,P.Cuotas,
              P.NombreImagen,P.Estado,P.FechaPresentacion,P.Puntaje,P.TasaInteres,P.Tipo
              FROM Proyectos P;", cn);

            // JOIN LEFT mágico para poder traer la tabla completa

            try
            {
                if (unaCon.AbrirConexion(cn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Proyecto> listaProyectos = new List<Proyecto>();
                    RepositorioUsuarios repoUsarios = new RepositorioUsuarios();
                    while (dr.Read())
                    {

                        listaProyectos.Add(new Proyecto
                        {
                            id = (int)dr["Id"],
                            // solicitante = repoUsarios.FindById((int)dr["Cedula"]), // findby id categoria
                            titulo = (string)dr["Titulo"],
                            descripcion = (string)dr["Descripcion"],
                            monto = (decimal)dr["Monto"],
                            cuotas = (int)dr["Cuotas"],
                            rutaImagen = (string)dr["NombreImagen"],
                            estado = (string)dr["Estado"],
                            fechaPresentacion = (DateTime)dr["FechaPresentacion"],
                            puntaje = (int)dr["Puntaje"],
                            tasaInteres = (decimal)dr["TasaInteres"]

                        });

                    }
                    return listaProyectos;
                }
                return null;
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

            finally
            {
                unaCon.CerrarConexion(cn);
            }

        }


        public IEnumerable<Proyecto> ProyectosPorUsuario(string cedula)
        {
      
            Conexion unaCon = new Conexion();

            SqlConnection cn = unaCon.CrearConexion();

            SqlCommand cmd = new SqlCommand(@"SELECT P.Id,P.Cedula,P.Titulo,P.Descripcion,P.Monto,P.Cuotas,
              P.NombreImagen,P.Estado,P.FechaPresentacion,P.Puntaje,P.TasaInteres,P.Tipo,PE.Experiencia,C.CantIntegrantes
                 FROM Proyectos P
                 LEFT JOIN Personales PE ON P.Id = PE.Id 
                 LEFT JOIN Cooperativos C ON P.Id = C.Id
                 WHERE P.Cedula=@cedula;", cn);

            cmd.Parameters.AddWithValue("@cedula", cedula);

            // JOIN LEFT mágico para poder traer la tabla completa

            try
            {
                if (unaCon.AbrirConexion(cn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Proyecto> listaProyectos = new List<Proyecto>();
                    while (dr.Read())
                    {
                        RepositorioProyectos repoProyectos = new RepositorioProyectos();
                        if (dr["Tipo"].ToString() == "PERSONAL")
                        {
                            listaProyectos.Add(new Personal
                            {
                                id= (int)dr["Id"],
                                //cedula
                                titulo= (string)dr["Titulo"],
                                descripcion = (string)dr["Descripcion"],
                                monto=(decimal)dr["Monto"],
                                cuotas=(int)dr["Cuotas"],
                                rutaImagen = (string)dr["NombreImagen"],
                                estado = (string)dr["Estado"],
                                fechaPresentacion = (DateTime)dr["FechaPresentacion"],
                                puntaje = (int)dr["Puntaje"],
                                tasaInteres = (decimal)dr["TasaInteres"],
                                experiencia = (string)dr["Experiencia"]

                            });

                        }

                        else if (dr["Tipo"].ToString() == "COOPERATIVO")
                        {
                            listaProyectos.Add(new Cooperativo

                            {
                                id = (int)dr["Id"],
                                //cedula
                                titulo = (string)dr["Titulo"],
                                descripcion = (string)dr["Descripcion"],
                                monto = (decimal)dr["Monto"],
                                cuotas = (int)dr["Cuotas"],
                                rutaImagen = (string)dr["NombreImagen"],
                                estado = (string)dr["Estado"],
                                fechaPresentacion = (DateTime)dr["FechaPresentacion"],
                                puntaje = (int)dr["Puntaje"],
                                tasaInteres = (decimal)dr["TasaInteres"],
                                cantIntegrantes = (int)dr["CantIntegrantes"],

                            });

                        }

                    }
                    return listaProyectos;
                }
                return null;
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            
            finally
            {
                unaCon.CerrarConexion(cn);
            }

        }

        public Proyecto FindById(object idProyecto)
        {
            SqlConnection cn = new Conexion().CrearConexion();
            SqlCommand cmdCli = new SqlCommand(@"SELECT P.Id,P.Cedula,P.Titulo,P.Descripcion,P.Monto,P.Cuotas,
              P.NombreImagen,P.Estado,P.FechaPresentacion,P.Puntaje,P.TasaInteres,P.Tipo,PE.Experiencia,C.CantIntegrantes
                 FROM Proyectos P
                 LEFT JOIN Personales PE ON P.Id = PE.Id 
                 LEFT JOIN Cooperativos C ON P.Id = C.Id
                 WHERE P.Id=@idProyecto;", cn);

            cmdCli.Parameters.AddWithValue("@idProyecto", idProyecto);
            try
            {
                Proyecto u = new Proyecto();
                if (new Conexion().AbrirConexion(cn))
                {
                    SqlDataReader dr = cmdCli.ExecuteReader();
                   
                    if (dr.Read())
                    {
                        if (dr["Tipo"].ToString() == "PERSONAL")                          
                        {
                            RepositorioUsuarios repoUser = new RepositorioUsuarios();
                            Personal p = new Personal();
                            p.id = (int)dr["Id"];
                            p.solicitante = (Solicitante)repoUser.FindById(dr["Cedula"]); 
                            p.titulo = (string)dr["Titulo"];
                            p.descripcion = (string)dr["Descripcion"];
                            p.monto = (decimal)dr["Monto"];
                            p.cuotas = (int)dr["Cuotas"];
                            p.rutaImagen = (string)dr["NombreImagen"];
                            p.estado = (string)dr["Estado"];
                            p.fechaPresentacion = (DateTime)dr["FechaPresentacion"];
                            p.puntaje = (int)dr["Puntaje"];
                            p.tasaInteres = (decimal)dr["TasaInteres"];
                            p.experiencia = (string)dr["Experiencia"];

                            return p;


                        }
                        else if (dr["Tipo"].ToString() == "COOPERATIVO")
                        {
                            RepositorioUsuarios repoUser = new RepositorioUsuarios();
                            Cooperativo c = new Cooperativo();
                            c.id = (int)dr["Id"];
                            c.solicitante = (Solicitante)repoUser.FindById(dr["Cedula"]);
                            c.titulo = (string)dr["Titulo"];
                            c.descripcion = (string)dr["Descripcion"];
                            c.monto = (decimal)dr["Monto"];
                            c.cuotas = (int)dr["Cuotas"];
                            c.rutaImagen = (string)dr["NombreImagen"];
                            c.estado = (string)dr["Estado"];
                            c.fechaPresentacion = (DateTime)dr["FechaPresentacion"];
                            c.puntaje = (int)dr["Puntaje"];
                            c.tasaInteres = (decimal)dr["TasaInteres"];
                            c.cantIntegrantes = (int)dr["CantIntegrantes"];

                            return c;


                        }
                    }
                   
                }
                return u;

            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                new Conexion().CerrarConexion(cn);
            }
        }


        private Usuario CargarClienteDesdeFila(IDataRecord dr)
        {

            /*int numColumnaApellido = dr.GetOrdinal("Apellido");
			string apellido = dr.GetString(numColumnaApellido);*/

            Usuario usu = new Usuario();
            usu.cedula = dr["Cedula"] != DBNull.Value ? dr["Cedula"].ToString() : "No tenés cedula!";
            usu.apellido = dr["Apellido"] != DBNull.Value ? dr["Apellido"].ToString() : "No tenés apellido!";
            usu.nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : "No tenés nombre!";
            usu.password = dr["Password"] != DBNull.Value ? dr["Password"].ToString() : "No tenés Password!";
            usu.fechaNacimiento = (DateTime)dr["FechaNacimiento"];

            return usu;
        }

        public List<int> CargarCuotas()
        {
            Conexion unaCon = new Conexion();
            SqlConnection cn = new Conexion().CrearConexion();
            SqlCommand cmd = new SqlCommand(@"SELECT c.Cuotas FROM Cuota_Tasa c;", cn);

            try
            {
                if (unaCon.AbrirConexion(cn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<int> listaCuotas = new List<int>();
                    while (dr.Read())
                    {
                        listaCuotas.Add((int)dr["Cuotas"]);

                    }
                    return listaCuotas;
                }
                return null;
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

            finally
            {
                unaCon.CerrarConexion(cn);
            }

        }

        bool IRepositorio<Proyecto>.Remove(Proyecto unT)
        {
            throw new NotImplementedException();
        }

        bool IRepositorio<Proyecto>.Update(Proyecto unT)
        {
            throw new NotImplementedException();
        }


     
        public bool Remove(Proyecto unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Proyecto unProyecto, string comentarios, string ciAdmin)
        {

            //Tendría todo el código de ADO.NET para hacer el INSERT  a través de comandos.

            if (unProyecto == null || !unProyecto.Validar())
                return false;
            string estado = "Aprobado";
            if (unProyecto.puntaje < 6) {
                estado = "Rechazado";
            }

            int id = unProyecto.id;
            Conexion unaCon = new Conexion();
            SqlConnection cn = unaCon.CrearConexion();
            SqlCommand cmd = new SqlCommand("UPDATE Proyectos SET Estado=@Estado, Puntaje=@Puntaje where Id=@id ;" +
                "INSERT INTO CambioEstado VALUES (@Id,@Cedula,@Comentarios,@Fecha);", cn);

            cmd.Parameters.Add(new SqlParameter("@Estado", estado));
            cmd.Parameters.Add(new SqlParameter("@Puntaje", unProyecto.puntaje));
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@Cedula", ciAdmin));
            cmd.Parameters.Add(new SqlParameter("@Comentarios", comentarios));
            cmd.Parameters.Add(new SqlParameter("@Fecha", DateTime.Now));

            try
            {
                if (unaCon.AbrirConexion(cn))
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
              return false;
            }
            finally
            {
                unaCon.CerrarConexion(cn);
            }

        }


        

        public IEnumerable<Proyecto> Filtrar(string cedula, DateTime fecha, string estado, string texto)
        {


            SqlConnection cn = new Conexion().CrearConexion();
            SqlCommand cmdCli = new SqlCommand(@"SELECT P.Id,P.Cedula,P.Titulo,P.Descripcion,P.Monto,P.Cuotas,
              P.NombreImagen,P.Estado,P.FechaPresentacion,P.Puntaje,P.TasaInteres
              FROM Proyectos P
                 
                 WHERE 1=1   ", cn);
            if (cedula !=null || cedula != "") {
                cmdCli.CommandText += "AND  P.Cedula=@Cedula";
                cmdCli.Parameters.AddWithValue("@Cedula", cedula);
            }
            if (fecha != null)
            {
                cmdCli.CommandText += " AND  P.FechaPresentacion=@Fecha";
                cmdCli.Parameters.AddWithValue("@Fecha", fecha);
            }
            if (estado != null || estado != "")
            {
                cmdCli.CommandText += " AND  P.Estado=@Estado";
                cmdCli.Parameters.AddWithValue("@Estado", estado);
            }
            if (texto != null || texto != "")
            {
                cmdCli.CommandText += " AND  P.Titulo LIKE '%@Texto%'";
                cmdCli.CommandText += "OR  P.Descripcion LIKE '%@Texto%'";
                cmdCli.Parameters.AddWithValue("@Texto", texto);
            }


            try
            {
                Proyecto u = new Proyecto();
                List<Proyecto> proyectosfilt = new List<Proyecto>();
                if (new Conexion().AbrirConexion(cn))
                {
                    SqlDataReader dr = cmdCli.ExecuteReader();

                    while (dr.Read())
                    {

                        proyectosfilt.Add(new Proyecto
                        {
                            id = (int)dr["Id"],
                            // solicitante = repoUsarios.FindById((int)dr["Cedula"]), // findby id categoria
                            titulo = (string)dr["Titulo"],
                            descripcion = (string)dr["Descripcion"],
                            monto = (decimal)dr["Monto"],
                            cuotas = (int)dr["Cuotas"],
                            rutaImagen = (string)dr["NombreImagen"],
                            estado = (string)dr["Estado"],
                            fechaPresentacion = (DateTime)dr["FechaPresentacion"],
                            puntaje = (int)dr["Puntaje"],
                            tasaInteres = (decimal)dr["TasaInteres"]

                        });

                    }
                    return proyectosfilt;

                }
                return null;
            }
            catch (SqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                new Conexion().CerrarConexion(cn);
            }
        }

    }



}

