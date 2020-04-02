using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Taller.Models.ViewModels
{

    public class ClienteViewModel  
    {
        public int IdCliente { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required]
        [StringLength(50)]
        public string Apellidos { get; set; }
        [Display(Name = "Cédula")]
        [Required]
        [Index(IsUnique = true)]
        public Nullable<int> Cedula { get; set; }
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:####-####}")]
        [StringLength(20)]
        public string Telefono { get; set; }
        [Display(Name = "Correo")]
        [Required]
        [EmailAddress]
        [StringLength(50)]
        
        public string Correo { get; set; }
        public EmpresaViewModel Empresa { get; set; }

    }
    public class EmpresaViewModel 
    {
        public int IdEmpresa { get; set; }

        [Display(Name = "Empresa")]
        [StringLength(50)]
        [Required]
        public string NombreEmpresa { get; set; }

        [Display(Name = "Ced. Juridica")]
        [StringLength(50)]
        public string CedJuridica { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(120)]
        public string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:9999-9999}")]
        [StringLength(20)]
        public string TelEmpresa { get; set; }

        public int? IdCliente { get; set; }
    }


}