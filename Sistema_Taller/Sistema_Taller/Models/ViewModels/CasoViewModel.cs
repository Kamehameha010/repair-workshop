using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Taller.Models.ViewModels
{
    public class CasoViewModel
    {

        public int IdCaso { get; set; }
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

        public List<CasoDetalleViewModel> Detalles{ get; set; }

    }

    public class CasoDetalleViewModel
    {
        public int IdCasoDetalle { get; set; }
        [Display(Name = "Detalle")]
        [StringLength(200)]
        public string Detalle { get; set; }
        [Display(Name = "Diagnostico")]
        [StringLength(200)]
        public string Diagnostico { get; set; }
        [Display(Name = "Estado")]
        public Nullable<int> IdEstadoCaso { get; set; }

        [Display(Name = "Fec. Despacho")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaDespacho { get; set; }

        public ArticuloViewModel Articulo { get; set; }

    }

    public class RepuestoUsadoViewModel
    {
        
        public int IdRespuesto { get; set; }
        [Display(Name = "Repuesto")]
        public Nullable<int> IdInvRep { get; set; }
        [Display(Name = "Cantidad")]
        public Nullable<int> Cantidad { get; set; }

    }

    public class EstadoCasoViewModel {

        public int IdEstadoCaso { get; set; }
        public string Descripcion { get; set; }

    }

}