using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Metadata
{
    public class Metadatas
    {
    }
    public class InventarioRepuestoMetadata
    {
        public int idInvRep { get; set; }
        [Display(Name = "Código")]
        [StringLength(20)]
        public string codigo { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string descripcion { get; set; }
        [Display(Name = "Precio")]
        public Nullable<decimal> precio { get; set; }
        [Display(Name = "Cantidad")]
        public Nullable<int> cantidad { get; set; }


    }
    public class ProveedorMetadata
    {

        [Display(Name = "Proveedor")]
        [StringLength(50)]
        [Required]
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