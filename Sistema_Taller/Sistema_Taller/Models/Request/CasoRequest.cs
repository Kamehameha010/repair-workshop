using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class CasoRequest
    {
        public int IdCaso { get; set; }
        public List<CasoDetalleRequest> Detalles { get; set; }
    }

    public class CasoDetalleRequest
    {
        public int IdCasoDetalle { get; set; }
        public int IdArticulo { get; set; }
        [Display(Name = "Detalle")]
        [StringLength(200)]
        public string Detalle { get; set; }
        [Display(Name = "Diagnostico")]
        [StringLength(200)]
        public string Diagnostico { get; set; }
        [Display(Name = "Estado")]
        public Nullable<int> IdEstadoCaso { get; set; }

    }
}