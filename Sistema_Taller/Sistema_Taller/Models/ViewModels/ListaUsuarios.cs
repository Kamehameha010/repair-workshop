using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.ViewModels
{
    public class ListaUsuarios
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public Nullable<int> Cedula { get; set; }

        public string Correo { get; set; }

        public string Username { get; set; }
        public string Rol { get; set; }

    }
}