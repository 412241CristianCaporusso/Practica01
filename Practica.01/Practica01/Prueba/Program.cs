using Practica01.Servicios;
using Practica01.Properties;
using Practica01.Dominio;
using System.Reflection.Metadata.Ecma335;



ComercioManager servicoioManager = new ComercioManager();


//Obtener todas la facturas

Console.WriteLine("Listado de facturas:");
var facturas = servicoioManager.GetFacturas();
foreach (var factura in facturas)
{
    Console.WriteLine($"Fecha: {factura.Fecha}, Forma de pago: {factura.FormaDePago.nombre}, Cliente {factura.Cliente}, Nro Factura {factura.NroFactura}");
}

Console.WriteLine("Listado de Detalles: ");
var detalles = servicoioManager.GetDetalles();
foreach (var detalle in detalles)
{
    Console.WriteLine($"ID: {detalle.id}, Articulo: {detalle.Articulo.nombre}, Cantidad: {detalle.cantidad}, Nro Factura {detalle.nro_factura}");
}


var formaDePago = new FormaDePago
{
    id = 2,
 
};

var oArticulo = new Articulo
{
    id=2,

};
var oArticulo2 = new Articulo
{
    id = 3
};

var lstdetalles = new List<Detalle>
{
    new Detalle {
    Articulo = oArticulo,
    cantidad = 3,
    },
    new Detalle
    {
        Articulo = oArticulo2,
        cantidad = 4,
    }
};

var nuevaFactura = new Factura
{
   
    Fecha = DateTime.Now,
    FormaDePago= formaDePago,
    Cliente = "Carlos Guzman",
    Detalles = lstdetalles

};

bool facturaCreada = servicoioManager.AddFactura(nuevaFactura);
Console.WriteLine("Factura creada: " + facturaCreada);
