using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class CasoController : Controller
    {
        // GET: Caso
        public ActionResult Index()
        {


            return View();
        }

        [HttpGet]
        public ActionResult Crear()
        {
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                ViewBag.estados = new SelectList(db.EstadoCaso.ToList(), "idEstadoCaso", "descripcion");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Crear(CasoViewModel model)
        {

            return View(model);
        }
    }
}