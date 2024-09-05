using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Dominio
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaDePago FormaDePago { get; set; }
        public string Cliente { get; set; }
        public List<Detalle> Detalles { get; set; } = new List<Detalle>();

    }
}
