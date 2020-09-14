using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Repositorios
{
	public class Conexion
	{
		private string cadenaConexion =ConfigurationManager.ConnectionStrings["miConexion"].ConnectionString;

        public SqlConnection CrearConexion()
		{
			return new SqlConnection(cadenaConexion);
		}
		public bool AbrirConexion(SqlConnection cn)
		{
			try
			{
				if (cn.State != System.Data.ConnectionState.Open)
				{
					cn.Open();
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{

				return false;
			}
			 

		}
		public bool CerrarConexion(SqlConnection cn)
		{
			try
			{
				if (cn.State != System.Data.ConnectionState.Closed)
				{
					cn.Close();
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				return false;
			}

		}
	}
}
