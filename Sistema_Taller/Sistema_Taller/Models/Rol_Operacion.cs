//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistema_Taller.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rol_Operacion
    {
        public int idRol_Ope { get; set; }
        public Nullable<int> idRol { get; set; }
        public Nullable<int> idOperacion { get; set; }
    
        public virtual Operacion Operacion { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
