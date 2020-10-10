using System;

namespace Dominio
{
	public class Usuario
	{
		public String cedula{ get; set; }

		public String nombre{ get; set; }

		public String apellido{ get; set; }// nuevo cambio

        public DateTime fechaNacimiento { get; set; } //hola algo nuevo

		public string password{ get; set; } //cambio rodrigo


        public bool Validar()
        {
            return true;
        }

        public int Edad()
        {
            int anios = 0;
            anios = DateTime.Now.Year - this.fechaNacimiento.Year;
            return anios;
        }

    }

}

