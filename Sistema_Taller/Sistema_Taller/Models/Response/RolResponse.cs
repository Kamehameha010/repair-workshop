using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Response
{
    public class RolResponse
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }

        public List<OperacionesResponse> Permisos { get; set; }
    }

    public class OperacionesResponse
    {
        public int IdOpeRol { get; set; }
        public int? IdRol { get; set; }
        public int? IdOperacion { get; set; }

    }
}