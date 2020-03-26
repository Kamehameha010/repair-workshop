using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.Request
{
    public class LoginRequest
    {
        public String Username { get; set; }
        public String Contrasena { get; set; }
    }
}