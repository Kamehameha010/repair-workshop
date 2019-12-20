using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Taller.Models.ViewModels
{
    public class ArticuloViewModel
    {

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Campo nombre es requerido")]
        [StringLength(50)]
        public string nombre { get; set; }

        [Display(Name = "Código")]
        [StringLength(50)]
        public string codigo { get; set; }

        [Display(Name = "Modelo")]
        [StringLength(50)]
        public string modelo { get; set; }

        [Display(Name = "Marca")]
        public Nullable<int> idMarca { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Campo categoría es requerido")]
        public Nullable<int> idCategoria { get; set; }

        [Display(Name = "Serie")]
        [StringLength(50)]
        public string serie { get; set; }
    }

    public class MarcaViewModel
    {
        [Display(Name = "Marca")]
        [StringLength(50)]
        [Required]
        public String nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(80)]
        [Required]
        public string descripcion { get; set; }
    }

    public class CategoriaViewModel
    {
        [Display(Name = "Categoría")]
        [StringLength(50)]
        [Required]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(80)]
        [Required]
        public string descrip { get; set; }
    }
}