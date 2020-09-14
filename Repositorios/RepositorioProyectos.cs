using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Repositorios
{
    class RepositorioProyectos : IRepositorio<Proyecto>
    {
        public bool Add(Proyecto unT)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proyecto> FindAll()
        {
            throw new NotImplementedException();
        }

        public Proyecto FindById(object clave)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Proyecto unT)
        {
            throw new NotImplementedException();
        }

        public bool Update(Proyecto unT)
        {
            throw new NotImplementedException();
        }
    }
}
