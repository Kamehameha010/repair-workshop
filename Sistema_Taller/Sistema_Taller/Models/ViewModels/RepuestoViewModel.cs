using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Taller.Models.ViewModels
{
    public class RepuestoViewModel
    {
        [Display(Name = "Código")]
        [StringLength(20)]
        [Required]
        public string codigo { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string descripcion { get; set; }
        [Display(Name = "Precio")]
        public Nullable<decimal> precio { get; set; }
        [Display(Name = "Cantidad")]
        public Nullable<int> cantidad { get; set; }
    }

    public partial class ProveedorRepuesto
    {

        [Display(Name = "Proveedor")]
        [StringLength(50)]
        public string nombre { get; set; }
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:####-####}")]
        [StringLength(20)]
        public string telefono { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(100)]
        public string direccion { get; set; }
    }
}