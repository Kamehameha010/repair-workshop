using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.ViewModels.BuscarViewModel
{
    public class BuscarCliente
    {

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public Nullable<int> Cedula { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public List<EmpresaViewModel> Negocio { get; set; }
    }
    

    
}