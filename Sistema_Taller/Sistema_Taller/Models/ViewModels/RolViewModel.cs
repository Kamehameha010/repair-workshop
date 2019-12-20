using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Taller.Models.ViewModels
{
    public class RolViewModel
    {
        [Display(Name = "Rol")]
        [Required]
        [StringLength(30)]
        public string nombre { get; set; }

    }

    public class OperacionViewModel
    {
        //TODO
        [Display(Name = "Operación")]
        [StringLength(30)]
        public string descripcion { get; set; }

    }

    public class ModuloViewModel
    {
        //TODO
        [Display(Name = "Modulo")]
        [Required]
        public String nombre { get; set; }

        [Display(Name = "Descripcion")]
        [Required]
        [StringLength(100)]
        public String descripcion { get; set; }
    }

    public partial class Rol_Operacion
    {
        //TODO
        public int idRol_Ope { get; set; }
        public Nullable<int> idRol { get; set; }
        public Nullable<int> idOperacion { get; set; }

    }
}