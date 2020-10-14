using System;
using System.ComponentModel.DataAnnotations;
using Dominio;

namespace Dominio
{
	public class Proyecto
	{
        [Required]
        public string titulo{ get; set; }
        [Required]
        public string descripcion{ get; set; }
        [Required]
        public decimal monto{ get; set; }
        [Required]
        public int cuotas{ get; set; }
        [Required]
        public string rutaImagen{ get; set; }

        public string estado { get; set; } = "Pendiente";

        public DateTime fechaPresentacion { get; set; } = DateTime.Now;
        [Required]
        public int puntaje { get; set; } = 0;
        [Required]
        public int id{ get; set; }
       
        public decimal tasaInteres{ get; set; }
        private cambioEstado cambioEstado;
   
        public Solicitante solicitante { get; set; }
     
        public void setSolicitante(Solicitante u)
        {
            this.solicitante = u;
        }

        public bool Validar()
        {
            return true;
        }

    }

}

