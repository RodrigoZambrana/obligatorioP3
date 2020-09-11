using System;
using Dominio;

namespace Dominio
{
	public class Proyecto
	{
		public string titulo{ get; set; }

		public string descripcion{ get; set; }

		public decimal monto{ get; set; }

		public int cuotas{ get; set; }

		public string rutaImagen{ get; set; }

		public string estado{ get; set; }

		public DateTime fechaPresentacion{ get; set; }

		public int puntaje{ get; set; }

		public int id{ get; set; }

		public decimal tasaInteres{ get; set; }

		private cambioEstado cambioEstado;

		private Solicitante solicitante;

	}

}

