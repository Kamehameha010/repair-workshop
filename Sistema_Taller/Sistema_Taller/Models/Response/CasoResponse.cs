using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Response
{
    public class CasoResponse
    {
        public int IdCaso { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int? NumeroCaso { get; set; }
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public int? IdEstadoCaso { get; set; }
        public DateTime? FechaDespacho { get; set; }
        public List<CasoDetalleResponse> CasoDetalle { get; set; }
    }
}