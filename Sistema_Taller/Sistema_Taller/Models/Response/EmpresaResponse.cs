using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Response
{
    public class EmpresaResponse:ClienteResponse
    {
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string CedJuridica { get; set; }
        public string Direccion { get; set; }
        public string TelEmpresa { get; set; }

    }
}