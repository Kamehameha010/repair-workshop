using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class EmpresaRequest
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string CedJuridica { get; set; }
        public string TelEmpresa { get; set; }
        public string Direccion { get; set; }
        public int IdCliente { get; set; }
    }
}