using Practica01.Dominio;
using Practica01.Repositorio.Contratos;
using Practica01.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Repositorio.Implemenaciones
{
    public class DetallesFacturaRepositorio : IDetalleFacturaRepositorio
    {
        public List<Detalle> GetAll()
        {
            var detalles = new List<Detalle>();
            DataTable table = DataHelper.GetInstance().ExecuteSPQuery("Obtener_detalles_facturas", null);

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    var detalle = new Detalle
                    {
                        id = Convert.ToInt32(row["id_detalle"]),
                        nro_factura = Convert.ToInt32(row["nro_factura"]),
                        Articulo = new Articulo
                        {
                            id = Convert.ToInt32(row["articulo"]),
                            nombre = row["nombre"].ToString(),
                            precioUnitario = Convert.ToInt32(row["precio_unitario"])
                        },
                        cantidad = Convert.ToInt32(row["cantidad"])

                    };
                    detalles.Add(detalle);
                }
            }
            return detalles;
        }

        //public bool save(Detalle detalle)
        //{
    
        //     var parametros = new List<ParameterSQL>
        //            {
        //                new ParameterSQL("@id_detalle", detalle.id),
        //                new ParameterSQL("@articulo", detalle.Articulo.id ),
        //                new ParameterSQL("@cantidad",detalle.cantidad),
        //                new ParameterSQL("@nro_factura", detalle.nro_factura)
        //            };
        //        int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("Crear_Detalle", parametros);
        //        return filasAfectadas > 0;


        //}
    }
}
