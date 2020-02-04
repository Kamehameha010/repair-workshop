using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.ViewModels
{
    public class UseraViewModel
    {

        public int idUsuario { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required]
        public string apellidos { get; set; }
        [Display(Name = "Cédula")]
        [Index(IsUnique = true)]
        [Required]
        public int cedula { get; set; }
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }
        [Display(Name = "Correo")]
        public string correo { get; set; }
        [Display(Name = "Username")]
        [Index(IsUnique = true)]
        [Required]
        public string username { get; set; }
        [Display(Name = "Contrasena")]
        [Required]
        public string contrasena { get; set; }
    }
}