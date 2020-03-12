
using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class RepuestoController : Controller
    {
        // GET: Repuesto
        public ActionResult ListadoRepuestos()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Proveedores(string nombre)
        {

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var res = db.ProveedorRepuesto.Where(x => x.nombre.Contains(nombre))
                    .Select(x=>x.nombre)
                           .Take(5).ToList();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult Crear()
        {
            RepuestoViewModel repuesto = new RepuestoViewModel();
            repuesto.Repuestos = new InventarioRepuesto();
            return View(repuesto);
        }
        [HttpPost]
        public ActionResult Crear(ProveedorRepuesto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {


                    }
                    return Json("1");
                }
            }
            catch (Exception e)
            {

                return Json("0");
            }

            return View(model);
        }

    }
}