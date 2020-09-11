using System;
using Dominio;

namespace Dominio
{
	public class cambioEstado
	{
		public DateTime fecha{ get; set; }

		public string usuario{ get; set; }

		public string comentario{ get; set; }

		private Proyecto proyecto;

	}

}

