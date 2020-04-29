using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Response
{
    public class CasoDetalleResponse
    {
        public int IdCasoDetalle { get; set; }
        public int? IdCaso { get; set; }
        public int? IdArticulo { get; set; }
        public string Articulo { get; set; }
        public string Codigo { get; set; }
        public int? Marca { get; set; }
        public int? Categoria { get; set; }
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string Observacion { get; set; }
        public string Diagnostico { get; set; }

    }
}