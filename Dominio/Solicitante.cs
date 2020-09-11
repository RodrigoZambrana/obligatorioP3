using Dominio;
using System.Collections.Generic;

namespace Dominio
{
	public class Solicitante : Usuario
	{
		public string celular{ get; set; }

		public string email{ get; set; }

		public string contrasena{ get; set; }

		private ICollection<Proyecto> listaProyectos;

	}

}

