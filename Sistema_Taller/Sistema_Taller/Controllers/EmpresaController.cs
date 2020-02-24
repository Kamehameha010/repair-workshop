using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_Taller.Models.ViewModels;
using Sistema_Taller.Models;

namespace Sistema_Taller.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }
        

    }
}