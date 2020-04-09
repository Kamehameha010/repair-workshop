
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
    
public partial class Factura
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Factura()
    {

        this.Factura_Detalle = new HashSet<Factura_Detalle>();

    }


    public int idFactura { get; set; }

    public Nullable<System.DateTime> fechafacturacion { get; set; }

    public Nullable<int> idUsuario { get; set; }

    public Nullable<int> idCliente { get; set; }

    public Nullable<int> claveELectronica { get; set; }

    public Nullable<int> ConsecutivoElectronico { get; set; }

    public Nullable<decimal> total { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Factura_Detalle> Factura_Detalle { get; set; }

    public virtual Usuario Usuario { get; set; }

    public virtual Cliente Cliente { get; set; }

}

}
