using Dominio;

namespace Dominio
{
	public class Personal : Proyecto
	{
		public string experiencia{ get; set; }

        public override decimal CalcularPrecio()
        {
            //montomaximo hardcodeado
            decimal montoFinal = this.monto + (this.monto * this.tasaInteres/100);
            return montoFinal;
        }
    }
}

