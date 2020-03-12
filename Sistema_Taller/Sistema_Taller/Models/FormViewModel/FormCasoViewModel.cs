using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.FormViewModel
{
    public class FormCasoViewModel
    {
        public Caso Caso { get; }
        public Cliente Cliente { get; }
        public CasoDetalleViewModel CasoDetalle { get; }

    }

   
}