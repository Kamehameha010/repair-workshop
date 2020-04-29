using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Taller.Models.ViewModels
{
    public class RepuestoViewModel:ProveedorViewModel
    {
        public int IdInvRep { get; set; }

        [Display(Name ="Código")]
        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }
        
        [Display(Name ="Descripción")]
        [StringLength(50)]
        [Required]
        public string Descripcion { get; set; }
        [Display(Name="Precio")]
        [Required]
        public Nullable<decimal> Precio { get; set; }
        [Display(Name ="Cantidad")]
        [Required]
        public Nullable<int> Cantidad { get; set; }

    }
}