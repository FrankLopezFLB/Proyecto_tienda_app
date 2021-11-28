using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Proyecto_tienda_app.Models
{
    public class Producto
    {
        [Display(Name = "Código")]
        public int codigo { get; set; }
        [Display(Name = "Nombre")]
       
        [Required(ErrorMessage = "Ingresa un nombre correcto")]
        [RegularExpression("^[a-zA-Z ]+")]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Ingresa una descripción correcta")]
        [RegularExpression("^[a-zA-Z ]+")]
        public string descripcion { get; set; }
        [Display(Name = "Categoría")]
        [Required]
        public int idcategoria { get; set; }
        [Display(Name = "Stock")]
        [Required(ErrorMessage = "Ingresa un numero")]
        public int stock { get; set; }
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Ingresa un numero")]
        public decimal precio { get; set; }
        public int estado { get; set; }
        [Display(Name = "Ingrese una imagen")]
        public string rutaimg { get; set; }
        public string nomcategoria { get; set; }
    }
}