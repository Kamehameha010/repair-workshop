using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Response
{
    public class UsuarioResponse
    {

        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int cedula { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string username { get; set; }
        public Nullable<int> idRol { get; set; }
        public Nullable<int> idEstado { get; set; }
    }
}