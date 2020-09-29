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
            SqlCommand cmd = new SqlCommand("INSERT INTO Proyectos VALUES (@Titulo,@Descripcion,@Monto,@Cuotas,@rutaImagen,@Estado,@fechaPresentacion,@Puntaje);", cn);
            cmd.Parameters.Add(new SqlParameter("@Titulo", unProyecto.titulo));
            cmd.Parameters.Add(new SqlParameter("@Descripcion", unProyecto.descripcion));
            cmd.Parameters.Add(new SqlParameter("@Monto", unProyecto.monto));
            cmd.Parameters.Add(new SqlParameter("@Cuotas", unProyecto.cuotas));
            cmd.Parameters.Add(new SqlParameter("@rutaImagen", unProyecto.rutaImagen));
            cmd.Parameters.Add(new SqlParameter("@Estado", unProyecto.estado));
            cmd.Parameters.Add(new SqlParameter("@fechaPresentacion", unProyecto.fechaPresentacion));
            cmd.Parameters.Add(new SqlParameter("@Puntaje", unProyecto.puntaje));
            
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = cn;
            SqlTransaction trn = null;
            try
            {
                if (unaCon.AbrirConexion(cn))
                {
                    //Inicia la transacción
                    cmd.ExecuteNonQuery();

                    trn = cn.BeginTransaction();


                    //Asignarle la transacción a cada uno de los comandos
                    //que querés que sean transaccionales:
                    cmd.Transaction = trn;
                    //
                    //
                    //int idAsignado = (int)cmd.ExecuteScalar();
                    if (unProyecto is Personal)
                    {
                        Personal per = (Personal)unProyecto;
                        cmd2.CommandText = "INSERT INTO Personales VALUES (@Experiencia)";
                        cmd2.Parameters.Add(new SqlParameter("@Experiencia", per.experiencia));
                        //cmd2.Parameters.Add(new SqlParameter("@Pais", adm.Pais));
                        //cmd2.Parameters.Add(new SqlParameter("@ImpuestoImportacion", ProductoImportado.ImpuestoImportacion));

                    }
                    else if (unProyecto is Cooperativo)
                    {
                        Cooperativo coo = (Cooperativo)unProyecto;
                        cmd2.CommandText = "INSERT INTO Cooperativos VALUES (@cantIntegrantes)";
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

