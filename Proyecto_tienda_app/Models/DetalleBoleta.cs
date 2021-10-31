using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_tienda_app.Models
{
    public class DetalleBoleta
    {
        public int CodigoBoleta { get; set; }
        public int CodigoProd { get; set; }
        public decimal Monto { get; set; }
        public int Cantidad { get; set; }

    }
}