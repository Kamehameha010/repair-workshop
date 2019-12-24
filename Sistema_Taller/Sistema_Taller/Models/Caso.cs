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
    
    public partial class Caso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Caso()
        {
            this.CasoDetalle = new HashSet<CasoDetalle>();
        }
    
        public int idCaso { get; set; }
        public Nullable<System.DateTime> fecha_ingreso { get; set; }
        public Nullable<int> numero_caso { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<int> idCliente { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CasoDetalle> CasoDetalle { get; set; }
    }
}