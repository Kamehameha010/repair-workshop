using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Taller.Models.ViewModels
{
    public class ListaCasos
    {

        public int id { get; set; }
        public DateTime? fecha { get; set; }
        public int? casoNum { get; set; }
        public String usuario { get; set; }
        public String cliente { get; set; }
        public string articulo { get; set; }
        public string modelo { get; set; }
        public string marca { get; set; }
        public string serie { get; set; }
    }
}