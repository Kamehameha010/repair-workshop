using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Taller.Models.ViewModels
{
    public class RepuestoViewModel:ProveedorRepuesto
    {
        public InventarioRepuesto Repuestos { get; set; }
    }
}