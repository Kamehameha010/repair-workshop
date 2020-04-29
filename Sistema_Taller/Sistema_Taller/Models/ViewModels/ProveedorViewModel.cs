using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.ViewModels
{
    public class ProveedorViewModel
    {

        public int IdProveedor { get; set; }
        [Display(Name ="Nombre")]
        [StringLength(50)]
        [Required]
        public String Nombre { get; set; }
        [Display(Name = "Correo")]
        [StringLength(80)]
        [EmailAddress]
        
        public String Correo { get; set; }
        [Display(Name = "Teléfono")]
        [StringLength(20)]
        
        public String Telefono { get; set; }
        [Display(Name = "Dirección")]
        [StringLength(100)]
        
        public String Direccion { get; set; }
    }
}