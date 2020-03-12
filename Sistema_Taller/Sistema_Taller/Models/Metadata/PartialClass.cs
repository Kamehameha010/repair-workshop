using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sistema_Taller.Models;
using Sistema_Taller.Models.Metadata;

namespace Sistema_Taller.Models
{
    public class PartialClass
    {
    }

    [MetadataType(typeof(ProveedorMetadata))]
    public partial class ProveedorRepuesto { }

    [MetadataType(typeof(InventarioRepuestoMetadata))]
    public partial class InventarioRepuesto { }
}