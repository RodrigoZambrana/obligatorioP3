using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioConfiguraciones
    {
        public int MaximoCuotas()
        {
            return 36;

        }

        public int MinimoCuotas()
        {
            return 1;

        }

        public int MontoMaximo()
        {
            return 80000;

        }

        public int MontoMinimo()
        {
            return 1000;

        }

        public List <Cuota_Tasa> CuotasyTasas()
        {
            List<Cuota_Tasa> listaCuotasyTasas = new List<Cuota_Tasa>();
            listaCuotasyTasas.Add(new Cuota_Tasa{cuotas = 1,tasa = 1});
            listaCuotasyTasas.Add(new Cuota_Tasa { cuotas = 3, tasa = 3 });
            listaCuotasyTasas.Add(new Cuota_Tasa { cuotas = 5, tasa = 5 });
            listaCuotasyTasas.Add(new Cuota_Tasa { cuotas = 7, tasa = 7 });
            listaCuotasyTasas.Add(new Cuota_Tasa { cuotas = 9, tasa = 9 });
            listaCuotasyTasas.Add(new Cuota_Tasa { cuotas = 11, tasa = 15 });
            listaCuotasyTasas.Add(new Cuota_Tasa { cuotas = 18, tasa = 20 });
            return listaCuotasyTasas;

        }


        public Cuota_Tasa FindTasaYcuoutas(int cantidadCuotas) {

            foreach (Cuota_Tasa ct in CuotasyTasas()){
                if (ct.cuotas == cantidadCuotas){
                    return ct;
                }
            }

            return null;
        }

    }
}
