using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
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

                    //int idAsignado = (int)cmd.ExecuteScalar();
                    if (unUsuario is Admin)
                    {
                        

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

        public Usuario FindById(object cedula)
        {
            string c = cedula.ToString();


            SqlConnection cn = new Conexion().CrearConexion();
            SqlCommand cmdCli = new SqlCommand(@"SELECT U.Cedula,U.Nombre,U.Apellido,U.FechaNacimiento,U.Password,U.Tipo,S.Celular,S.Email,A.Cedula
           FROM Usuarios U
           LEFT JOIN Admin A ON U.Cedula = A.Cedula
           LEFT JOIN Solicitantes S ON U.Cedula = S.Cedula
            WHERE U.Cedula=@cedula;", cn);

            cmdCli.Parameters.AddWithValue("@cedula", c);
            try
            {
               Usuario usu = new Usuario();

                if (new Conexion().AbrirConexion(cn))
                {
                    SqlDataReader dr = cmdCli.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr["Tipo"].ToString() == "ADMIN")
                        {
                            Admin adm = new Admin();
                            adm.cedula = dr["Cedula"] != DBNull.Value ? dr["Cedula"].ToString() : "No tenés cedula!";
                            adm.nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : "No tenés nombre!";
                            adm.apellido = dr["Apellido"] != DBNull.Value ? dr["Apellido"].ToString() : "No tenés apellido!";
                            adm.fechaNacimiento = (DateTime)dr["FechaNacimiento"];
                            adm.password = dr["Password"] != DBNull.Value ? dr["Password"].ToString() : "No tenés Password!";
                           
                            return adm;

                        }
                        else if (dr["Tipo"].ToString() == "SOLICITANTE")
                        {

                            Solicitante sol = new Solicitante();
                            sol.cedula = dr["Cedula"] != DBNull.Value ? dr["Cedula"].ToString() : "No tenés cedula!";
                            sol.nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : "No tenés nombre!";
                            sol.apellido = dr["Apellido"] != DBNull.Value ? dr["Apellido"].ToString() : "No tenés apellido!";
                            sol.fechaNacimiento = (DateTime)dr["FechaNacimiento"];
                            sol.password = dr["Password"] != DBNull.Value ? dr["Password"].ToString() : "No tenés Password!";                           
                            sol.celular= dr["Celular"] != DBNull.Value ? dr["Celular"].ToString() : "No tenés Password!";
                            sol.email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : "No tenés Password!";
                            return sol;
                        }
                    }
                    if (dr.NextResult())
                    {
                        //while (dr.Read())
                        //{
                        //    usu.AgregarTelefono(
                        //        new Telefono
                        //        {
                        //            CodigoArea = (int)dr["CodArea"],
                        //            EsCelular = (bool)dr["EsCelular"],
                        //            Numero = (int)dr["Numero"]
                        //        }
                        //        );

                        //}
                    }
                }
                return usu;
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
