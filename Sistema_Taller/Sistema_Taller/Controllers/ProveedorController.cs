using Sistema_Taller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ProveedorRepuesto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        db.ProveedorRepuesto.Add(model);
                        db.SaveChanges();
                    }
                    return Json("1");
                }
                return View(model);

            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oProveedor = db.ProveedorRepuesto.Find(id);
                if (oProveedor == null)
                {
                    return HttpNotFound();
                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult Editar(ProveedorRepuesto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    return Json("1");
                }
                return View(model);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oProveedor = db.ProveedorRepuesto.Find(id);
                db.ProveedorRepuesto.Remove(oProveedor);
                db.SaveChanges();
            }
            return Json("1");
        }

        [HttpGet]

        public ActionResult BuscarProveedor(int? id) { 

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProveedorRepuesto oProveedor;
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oElement = db.ProveedorRepuesto.Find(id);

                if(oElement == null)
                {
                    return HttpNotFound();
                }

                oProveedor = new ProveedorRepuesto()
                {
                    idProveedor = oElement.idProveedor,
                    nombre = oElement.nombre,
                    telefono = oElement.telefono,
                    direccion = oElement.direccion
                };

            }
            return Json(oProveedor, JsonRequestBehavior.AllowGet);
        
        }
        
        [HttpPost]
        public ActionResult BuscarProveedo(string term)
        {

            if (term == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProveedorRepuesto oProveedor;
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oElement = db.ProveedorRepuesto.Where(x => x.nombre.Contains(term)).FirstOrDefault();

                if (oElement == null)
                {
                    return HttpNotFound();
                }

                oProveedor = new ProveedorRepuesto()
                {
                    idProveedor = oElement.idProveedor,
                    nombre = oElement.nombre,
                    telefono = oElement.telefono,
                    direccion = oElement.direccion
                };

            }
            return Json(oProveedor);

        }



    }
}


