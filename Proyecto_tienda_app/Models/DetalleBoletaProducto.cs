using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_tienda_app.Models
{
    public class DetalleBoletaProducto
    {
        public int CodigoBoleta { get; set; }
        public int CodigoProducto { get; set; }
        public String NombreProducto{ get; set; }
        public String RutaImgProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get; set; }

    }
}