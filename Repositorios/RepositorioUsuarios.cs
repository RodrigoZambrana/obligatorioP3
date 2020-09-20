using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioUsuarios : IRepositorio<Usuario>
    {
        public bool Add(Usuario unUsuario)
        {

            //Tendría todo el código de ADO.NET para hacer el INSERT  a través de comandos.

            if (unUsuario == null || !unUsuario.Validar())
                return false;

            Conexion unaCon = new Conexion();
            SqlConnection cn = unaCon.CrearConexion();
            SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios VALUES (@Cedula,@Nombre,@Apellido,@FechaNacimento,@Password,@Tipo);", cn);
            cmd.Parameters.Add(new SqlParameter("@Nombre", unUsuario.nombre));
            cmd.Parameters.Add(new SqlParameter("@Apellido", unUsuario.apellido));
            cmd.Parameters.Add(new SqlParameter("@Cedula", unUsuario.cedula));
            cmd.Parameters.Add(new SqlParameter("@FechaNacimento", unUsuario.fechaNacimiento));
            cmd.Parameters.Add(new SqlParameter("@Password", unUsuario.password));
            cmd.Parameters.Add(new SqlParameter("@Tipo", unUsuario.GetType().Name.ToUpper()));
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
                    if (unUsuario is Admin)
                    {
                        //Admin adm = (Admin)unUsuario;
                        //cmd2.CommandText = "INSERT INTO ProductoImportado VALUES (@Codigo,@Pais,@ImpuestoImportacion)";
                        //cmd2.Parameters.Add(new SqlParameter("@Codigo", -88));
                        //cmd2.Parameters.Add(new SqlParameter("@Pais", adm.Pais));
                        //cmd2.Parameters.Add(new SqlParameter("@ImpuestoImportacion", ProductoImportado.ImpuestoImportacion));

                    }
                    else if (unUsuario is Solicitante)
                    {
                        Solicitante s = (Solicitante)unUsuario;
                        cmd2.CommandText = "INSERT INTO Solicitantes VALUES (@Email,@Celular,@Cedula)";
                        cmd2.Parameters.Add(new SqlParameter("@Celular", s.celular));
                        cmd2.Parameters.Add(new SqlParameter("@Email", s.email));
                        cmd2.Parameters.Add(new SqlParameter("@Cedula", unUsuario.cedula));

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

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Usuario unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario unT)
        {
            throw new NotImplementedException();
        }
    }
}
