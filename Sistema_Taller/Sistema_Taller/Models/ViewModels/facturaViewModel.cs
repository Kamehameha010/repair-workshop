using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Taller.Models.ViewModels
{
    public class FacturaViewModel
    {
        [Display(Name = "Fecha ingreso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fechafacturacion { get; set; }
        [Display(Name = "Usuario")]
        public Nullable<int> idUsuario { get; set; }
        [Display(Name = "Cliente")]
        public Nullable<int> idCliente { get; set; }
        [Display(Name = "Clave Electronica")]
        public Nullable<int> claveELectronica { get; set; }
        [Display(Name = "Consecutivo Electronico")]
        public Nullable<int> ConsecutivoElectronico { get; set; }
        [Display(Name = "total")]
        public Nullable<decimal> total { get; set; }
    }

    public partial class Factura_DetalleViewModel
    {
        [Display(Name = "Caso")]
        public Nullable<int> idcasoDetalle { get; set; }
        [Display(Name = "Importe")]
        public Nullable<decimal> importe { get; set; }
        [Display(Name = "Descuento")]
        public Nullable<decimal> descuento { get; set; }
        [Display(Name = "iva")]
        public Nullable<decimal> iva { get; set; }

    }
}