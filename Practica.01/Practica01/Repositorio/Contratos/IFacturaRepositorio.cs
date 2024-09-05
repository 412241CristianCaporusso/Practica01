using Practica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Repositorio.Contratos
{
    public interface IFacturaRepositorio
    {
        Factura GetById(int id);
        List<Factura> GetAll();
        bool Save(Factura factura);

    }
}
