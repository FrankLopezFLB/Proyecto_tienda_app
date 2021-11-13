using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_tienda_app.Models
{
    public class Response
    {
        public bool HayError { get; set; }
        public string Mensaje { get; set; }
        public int FilasAfectadas { get; set; }
    }
}