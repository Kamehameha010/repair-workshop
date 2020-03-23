using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class RolRequest
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public  List<OperacionesRequest> Operaciones { get; set; }
    }

    public class OperacionesRequest {

        public int IdOpeRol { get; set; }
        public int? IdRol { get; set; }
        public int? IdOperacion { get; set; }

        
    }


}