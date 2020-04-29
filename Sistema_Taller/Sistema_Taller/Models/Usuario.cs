
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
    
public partial class Usuario
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Usuario()
    {

        this.Factura = new HashSet<Factura>();

        this.Caso = new HashSet<Caso>();

    }


    public int idUsuario { get; set; }

    public string nombre { get; set; }

    public string apellidos { get; set; }

    public int cedula { get; set; }

    public string telefono { get; set; }

    public string correo { get; set; }

    public string username { get; set; }

    public string contrasena { get; set; }

    public Nullable<int> idRol { get; set; }

    public Nullable<int> idEstado { get; set; }



    public virtual Estado Estado { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Factura> Factura { get; set; }

    public virtual Rol Rol { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Caso> Caso { get; set; }

}

}
