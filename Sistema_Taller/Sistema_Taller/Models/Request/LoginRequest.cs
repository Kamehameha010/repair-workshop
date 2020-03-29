using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class LoginRequest
    {
        [Display(Name ="Usuario")]
        [Required]
        public String Username { get; set; }
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required]
        
        public String Contrasena { get; set; }
    }
}