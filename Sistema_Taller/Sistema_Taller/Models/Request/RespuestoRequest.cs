using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class RespuestoRequest:ProveedorRepuesto
    {
        public List<InventarioRepuesto> Repuesto { get; set; }
    }
}