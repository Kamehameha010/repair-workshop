using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class RespuestoRequest
    {
        public int IdInvRep { get; set; }
        public int? IdProveedor { get; set; }
        [StringLength(20)]
        [Required]
        public string Codigo { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [Required]
        public Nullable<decimal> Precio { get; set; }
        
        [Required]
        public Nullable<int> Cantidad { get; set; }

    }
}