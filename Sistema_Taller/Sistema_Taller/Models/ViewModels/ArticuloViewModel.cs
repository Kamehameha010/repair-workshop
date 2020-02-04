using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Taller.Models.ViewModels
{
    public class ArticuloViewModel
    {
        public int IdArticulo { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Campo nombre es requerido")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Código")]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Display(Name = "Modelo")]
        [StringLength(50)]
        public string Modelo { get; set; }

        [Display(Name = "Marca")]
        public Nullable<int> IdMarca { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "Campo categoría es requerido")]
        public Nullable<int> IdCategoria { get; set; }

        [Display(Name = "Serie")]
        [StringLength(50)]
        public string Serie { get; set; }
    }

    public class MarcaViewModel
    {

        public int IdMarca { get; set; }
        [Display(Name = "Marca")]
        [StringLength(50)]
        [Required]
        public String Nombre { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(80)]
        
        public string Descripcion { get; set; }
    }

    public class CategoriaViewModel
    {
        public int IdCategoria { get; set; }
        [Display(Name = "Categoría")]
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(80)]
        
        public string Descripcion { get; set; }
    }
}