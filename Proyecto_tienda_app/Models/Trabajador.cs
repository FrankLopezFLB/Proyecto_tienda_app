using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_tienda_app.Models
{
    public class Trabajador:Usuario
    {
        public string Dni { get; set; }
        public int PuestoId { get; set; }
    }
}