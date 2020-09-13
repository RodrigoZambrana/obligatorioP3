using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioUsuarios
    {
        public bool Add(Usuario unUsuario)
        {
            //Tendría todo el código de ADO.NET para hacer el INSERT  a través de comandos.
            return true;
        }
        public bool Remove(Usuario unUsuario)
        {
            return true;
        }
        public bool Remove(int Id)
        {
            return true;
        }
        public bool Update(Usuario unUsuario)
        {
            //No se modifica la clave primaria, o identificador
            return true;
        }

        public Usuario FindById(int id)
        {
            //retorna la categoría con ese id
            return null;
        }
        public List<Usuario> FindAll()
        {
            return null;
        }
        public List<Usuario> FindXPalabraEnNombre(string nom)
        {
            //hay que hacer la búsqueda por nombre en las categorías.
            throw new NotImplementedException("Ups, te falta implementar este método");
            //return null;
        }

    }
}
