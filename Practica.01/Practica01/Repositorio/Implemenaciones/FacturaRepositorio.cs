using Practica01.Dominio;
using Practica01.Repositorio.Contratos;
using Practica01.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Repositorio.Implemenaciones
{
    public class FacturaRepositorio : IFacturaRepositorio
    {
        public List<Factura> GetAll()
        {

            var facturas = new List<Factura>();
            DataTable table = DataHelper.GetInstance().ExecuteSPQuery("Obtener_Facturas", null);

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    var factura = new Factura
                    {
                        NroFactura = Convert.ToInt32(row["nro_factura"]),
                        Fecha = (DateTime)row["fecha"],
                        FormaDePago = new FormaDePago
                        {
                            id = Convert.ToInt32(row["id_forma_pago"]),
                            nombre = row["nombre"].ToString(),
                        },
                        Cliente = row["cliente"].ToString()

                    };
                    facturas.Add(factura);
                }
            }
            return facturas;
        }

        public Factura GetById(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@nro_factura", id));
            DataTable tabla = DataHelper.GetInstance().ExecuteSPQuery("SP_Obtener_factura_por_id", parameters);

            if (tabla != null && tabla.Rows.Count == 1)
            {
                DataRow row = tabla.Rows[0];
                int nro_factura = Convert.ToInt32(row["nro_factura"]);
                DateTime fecha = Convert.ToDateTime(row["fecha"]);
                int formaPago = Convert.ToInt32(row["id_forma_pago"]);
                string nombreFP = Convert.ToString(row["nombre"]);
                string cliente = Convert.ToString(row["cliente"]);

                FormaDePago oFormaPago = new FormaDePago()
                {
                    id = formaPago,
                    nombre = nombreFP
                };

                Factura oFactura = new Factura()
                {
                    NroFactura = nro_factura,
                    Fecha = fecha,
                    FormaDePago = oFormaPago,
                    Cliente = cliente

                };
                return oFactura;

            }
            return null;
        }

        public bool Save(Factura factura)
        {
            //var parametros = new List<ParameterSQL>
            //    {
            //        new ParameterSQL("@nro_factura", factura.NroFactura),
            //        new ParameterSQL("@fecha",factura.Fecha ),
            //        new ParameterSQL("@id_forma_pago",factura.FormaDePago.id),
            //        new ParameterSQL("@cliente", factura.Cliente)
            //    };

            //int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("Crear_Factura", parametros);

            //return filasAfectadas > 0;


            bool result = true;
            SqlConnection cnn = null;
            SqlTransaction t = null;
            try
            {
                DataHelper.GetInstance();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_CREAR_FACTURA", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FECHA", factura.Fecha );
                cmd.Parameters.AddWithValue("@id_forma_pago", factura.FormaDePago.id);
                cmd.Parameters.AddWithValue("@cliente", factura.Cliente);


                SqlParameter param = new SqlParameter("@nro_factura", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();

                int nro_factura = (int)param.Value;

                int detalleId = 1;

                foreach (var detalle in factura.Detalles)

                {
                    var cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLES", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@ID", detalleId);
                    cmdDetalle.Parameters.AddWithValue("@ARTICULO", detalle.Articulo.id );
                    cmdDetalle.Parameters.AddWithValue("@CANTIDAD", detalle.cantidad );
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", nro_factura);

                    cmdDetalle.ExecuteNonQuery();

                    detalleId++;
                }
                t.Commit();

            }
            catch (SqlException)
            {
                if (t != null)
                    t.Rollback();

                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }


            return result;
        }
    }
}
