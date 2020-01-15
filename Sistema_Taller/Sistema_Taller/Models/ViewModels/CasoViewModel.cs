using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Taller.Models.ViewModels
{
    public class CasoViewModel
    {
        [Display(Name = "Fecha ingreso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public Nullable<System.DateTime> FechaIngreso { get; set; }

        [Display(Name = "Numero caso")]
        [Index(IsUnique = true)]
        public Nullable<int> NumeroCaso { get; set; }
        [Display(Name = "Usuario")]
        public Nullable<int> IdUsuario { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> IdCliente { get; set; }

    }

    public partial class CasoDetalleViewModel
    {
        [Display(Name = "Detalle")]
        [StringLength(200)]
        public string Detalle { get; set; }
        [Display(Name = "Diagnostico")]
        [StringLength(200)]
        public string Diagnostico { get; set; }
        [Display(Name = "Estado")]
        public Nullable<int> IdEstadoCaso { get; set; }

        [Display(Name = "Fecha ingreso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaDespacho { get; set; }

    }

    public partial class RepuestoUsadoViewModel
    {
        [Display(Name = "Repuesto")]
        public Nullable<int> idInvRep { get; set; }
        [Display(Name = "Cantidad")]
        public Nullable<int> cantidad { get; set; }

    }
}