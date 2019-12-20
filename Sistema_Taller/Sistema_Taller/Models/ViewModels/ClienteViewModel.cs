using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Taller.Models.ViewModels
{
    public class ClienteViewModel
    {
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }
        [Display(Name = "Apellidos")]
        [Required]
        [StringLength(50)]
        public string apellidos { get; set; }
        [Display(Name = "Cédula")]
        [Required]
        public Nullable<int> cedula { get; set; }
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:####-####}")]
        [StringLength(20)]
        public string telefono { get; set; }
        [Display(Name = "Correo")]
        [Required]
        [EmailAddress]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string correo { get; set; }


    }
    public class EmpleadoViewModel
    {

        [Display(Name = "Empresa")]
        [StringLength(50)]
        public string nombre { get; set; }

        [Display(Name = "Ced. Juridica")]
        [StringLength(50)]
        public string cedJuridica { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(120)]
        public string direccion { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:####-####}")]
        [StringLength(20)]
        public string telefono { get; set; }
    }
    public class ContactoViewModel
    {
        [Display(Name = "Nombre")]
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:####-####}")]
        [StringLength(20)]
        public string telefono { get; set; }
        [EmailAddress]
        public string correo { get; set; }
    }
}