using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Response
{
    public class ClienteResponse
    {

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int? Cedula { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}