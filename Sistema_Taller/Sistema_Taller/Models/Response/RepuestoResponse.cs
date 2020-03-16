using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Response
{
    public class RepuestoResponse:ProveedorRepuesto
    {
        public int idInventario { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<int> Cantidad { get; set; }
    }
}