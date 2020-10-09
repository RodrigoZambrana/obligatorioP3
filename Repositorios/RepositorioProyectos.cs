using System;
using System.Collections.Generic;
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

        IEnumerable<Proyecto> IRepositorio<Proyecto>.FindAll()
        {
            throw new NotImplementedException();
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

        Proyecto IRepositorio<Proyecto>.FindById(object clave)
        {
            throw new NotImplementedException();
        }

        bool IRepositorio<Proyecto>.Remove(Proyecto unT)
        {
            throw new NotImplementedException();
        }

        bool IRepositorio<Proyecto>.Update(Proyecto unT)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proyecto> FindAll()
        {
            throw new NotImplementedException();
        }

        public Proyecto FindById(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Proyecto unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Proyecto unT)
        {
            throw new NotImplementedException();
        }
    }

       
    }

