using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_tienda_app.Models
{
    public class Producto
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int idcategoria { get; set; }
        public int stock { get; set; }
        public decimal precio { get; set; }
        public int estado { get; set; }
        public string rutaimg { get; set; }
    }
}