using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_tienda_app.Models
{
    public class Usuario
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Ingresa un nombre correcto")]
        [RegularExpression("^[a-zA-Z ]+")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresa un apellido correcto")]
        [RegularExpression("^[a-zA-Z ]+")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingresa un telefono correcto")]
        [RegularExpression("^[0-9]{7,9}")]
        public string Telefono { get; set; }

        
        [RegularExpression("^[a-zA-Z 0-9]+")]
        [Required(ErrorMessage = "Ingresa una direccion correcto")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingresa un correo correcto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingresa una clave correcta")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "Ingresa un dni correcto")]
        [RegularExpression("^[0-9]{8}")]
        public string Dni { get; set; }

        public int Estado { get; set; }
        public int puestoID { get; set; }
        [Display(Name = "Puesto")]
        public string nombrePuesto { get; set; }
    }
}