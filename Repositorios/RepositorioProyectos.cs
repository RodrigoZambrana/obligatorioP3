using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Repositorios
{
    class RepositorioProyectos
    {
        public bool Add(Proyecto unProyecto)
        {
            //Tendría todo el código de ADO.NET para hacer el INSERT  a través de comandos.
            return true;
        }
        public bool Remove(Proyecto unProyecto)
        {
            return true;
        }
        public bool Remove(int Id)
        {
            return true;
        }
        public bool Update(Proyecto unProyecto)
        {
            //No se modifica la clave primaria, o identificador
            return true;
        }

        public Proyecto FindById(int id)
        {
            //retorna la categoría con ese id
            return null;
        }
        public List<Proyecto> FindAll()
        {
            return null;
        }
        public List<Proyecto> FindXPalabraEnNombre(string nom)
        {
            //hay que hacer la búsqueda por nombre en las categorías.
            throw new NotImplementedException("Ups, te falta implementar este método");
            //return null;
        }
    }
}
