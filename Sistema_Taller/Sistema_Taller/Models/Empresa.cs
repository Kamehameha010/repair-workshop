
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
    
public partial class Empresa
{

    public int idEmpresa { get; set; }

    public string nombre { get; set; }

    public string cedJuridica { get; set; }

    public string direccion { get; set; }

    public string telefono { get; set; }

    public Nullable<int> idCliente { get; set; }



    public virtual Cliente Cliente { get; set; }

}

}
