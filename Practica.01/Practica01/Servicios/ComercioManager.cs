using Practica01.Dominio;
using Practica01.Repositorio.Contratos;
using Practica01.Repositorio.Implemenaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Servicios
{
    public class ComercioManager
    {
        IDetalleFacturaRepositorio detalleFacturaRepositorio;
        IFacturaRepositorio facturaRepositorio;
        ITipoFormaPago tipoFormaPago;

        public ComercioManager()
        {
            detalleFacturaRepositorio = new DetallesFacturaRepositorio();
            facturaRepositorio = new FacturaRepositorio();
            tipoFormaPago = new FormaDePagoRepositorio();

        }
        public List<Factura> GetFacturas()
        {
            return facturaRepositorio.GetAll();
        }
        public bool AddFactura(Factura factura)
        {
            return facturaRepositorio.Save(factura);
        }
        public Factura GetFactura(int id)
        {
            return facturaRepositorio.GetById(id);
        }

        public List<Detalle> GetDetalles()
        {
            return detalleFacturaRepositorio.GetAll();
        }
        public bool guadarDetalle(Detalle detalle)
        {
            return detalleFacturaRepositorio.save(detalle);
        }
        public List<FormaDePago> GetFormaDePagos()
        {
            return tipoFormaPago.GetAll();
        }


    }
}
