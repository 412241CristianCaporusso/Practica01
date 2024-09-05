using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Dominio
{
    public class Detalle
    {

        public int id { get; set; }
        public Articulo Articulo { get; set; }
        public int cantidad { get; set; }
        public int nro_factura { get; set; }

    }
}
