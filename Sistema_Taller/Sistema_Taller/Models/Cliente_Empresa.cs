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
    
    public partial class Cliente_Empresa
    {
        public int idCliente_Emp { get; set; }
        public Nullable<int> idCliente { get; set; }
        public Nullable<int> idEmpresa { get; set; }
        public Nullable<int> idContacto { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Contacto Contacto { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}