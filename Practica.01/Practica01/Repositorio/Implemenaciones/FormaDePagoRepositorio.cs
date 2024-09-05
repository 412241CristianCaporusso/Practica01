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
    public class FormaDePagoRepositorio : ITipoFormaPago
    {
        public List<FormaDePago> GetAll()
        {
            DataTable table = DataHelper.GetInstance().ExecuteSPQuery("Obtener_formas_pago", null);
            var tiposFP = new List<FormaDePago>();

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    var tipo = new FormaDePago
                    {
                        id = (int)row["id_forma_pago"],
                        nombre = row["nombre"].ToString()
                    };
                    tiposFP.Add(tipo);
                }
            }
            return tiposFP;
        }
    }
}
