﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sistema_Taller.Models.ViewModels
{
    public class UsuarioViewModel 
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
        [Index(IsUnique =true)]
        [Required]
        public int cedula { get; set; }
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:####-####}")]
        [StringLength(20)]
        public string telefono { get; set; }
        [Display(Name ="Correo")]
        [EmailAddress]
        [StringLength(50)]
        public string correo { get; set; }
        [Display(Name = "Usuario")]
        [Index(IsUnique =true)]
        [Required]
        [StringLength(30)]
        public string username { get; set; }
        [Display(Name = "Contraseña")]
        [Required]
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