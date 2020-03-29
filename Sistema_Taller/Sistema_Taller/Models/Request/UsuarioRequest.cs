using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class UsuarioRequest
    {
        public int idUsuario { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(50)]

        public string nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required]
        [StringLength(50)]
        public string apellidos { get; set; }
        [Display(Name = "Cédula")]
        public string cedula { get; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:####-####}")]
        [StringLength(20)]
        public string telefono { get; set; }
        [Display(Name = "Correo")]
        [EmailAddress]
        [StringLength(50)]
        public string correo { get; set; }
        [Display(Name = "Usuario")]
        [Index(IsUnique = true)]
        [Required]
        [StringLength(30)]
        public string username { get; set; }
        [Display(Name = "Contraseña")]
        [StringLength(50)]
        public string contrasena { get; set; }
        [Display(Name = "Rol")]
        [Required]
        public Nullable<int> idRol { get; set; }
        [Display(Name = "Estado")]
        [Required]
        public Nullable<int> idEstado { get; set; }
    }
}