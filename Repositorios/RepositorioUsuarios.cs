using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioUsuarios : IRepositorio<Usuario>
    {
        public bool Add(Usuario unT)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> FindAll()
        {
            throw new NotImplementedException();
        }

        public Usuario FindById(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Usuario unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Usuario unT)
        {
            throw new NotImplementedException();
        }
    }
}
