using System;
using Dominio;

namespace Dominio
{
    public class Cooperativo : Proyecto
    {
        public int cantIntegrantes { get; set; }

        public override decimal CalcularPrecio()
        {
            decimal montoFinal = this.monto + (this.monto * this.tasaInteres / 100);
            return montoFinal;
        }

        public decimal PorcentajeAdicional() {
            if (this.cantIntegrantes < 10)
            {
                decimal porcentaje = (decimal)0.02 * this.cantIntegrantes;
                return porcentaje;
            }
            else
            {
               decimal porcentaje = (decimal)0.2;
                return porcentaje;
            }
                 
        }


    }
}

