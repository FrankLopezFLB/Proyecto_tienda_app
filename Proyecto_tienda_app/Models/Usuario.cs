using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_tienda_app.Models
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string Dni { get; set; }
        public int Estado { get; set; }
        public int puestoID { get; set; }
        public string nombrePuesto { get; set; }
    }
}