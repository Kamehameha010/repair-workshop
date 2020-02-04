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
        [HttpPost]
         public JsonResult ListadoClienteNegocios()
        {
            List<View_Negocio> negocios;
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                negocios = db.View_Negocio.ToList();
            }
            return Json(negocios);

        }
    }
}