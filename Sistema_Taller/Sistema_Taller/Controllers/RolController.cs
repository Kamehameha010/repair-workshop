using Sistema_Taller.Models;
using Sistema_Taller.Models.Request;
using Sistema_Taller.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Crear(RolRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        Rol rol = new Rol()
                        {
                            nombre = model.NombreRol
                        };

                        db.Rol.Add(rol);
                        db.SaveChanges();

                        foreach (var oOperacion in model.Operaciones)
                        {
                            Rol_Operacion asignacionRol = new Rol_Operacion() {
                                idRol = rol.idRol,
                                idOperacion = oOperacion.IdOperacion
                            };
                            db.Rol_Operacion.Add(asignacionRol);
                        }
                        db.SaveChanges();
                    }
                    return Content("1");
                }
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }


        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oRol = db.Rol.Find(id);
                if (oRol == null)
                {
                    return HttpNotFound();
                }

                return View();
            }

        }
        [HttpPost]
        public ActionResult Editar(RolRequest model) {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities()) {
                    var oRol = db.Rol.Find(model.IdRol);
                    oRol.nombre = model.NombreRol;
                    db.Entry(oRol).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    var antPermiso = db.Rol_Operacion.Where(x => x.idRol == oRol.idRol).ToList();
                    foreach (var oElement in antPermiso) {
                        db.Rol_Operacion.Remove(oElement);
                    }
                    db.SaveChanges();

                    foreach (var oElement in model.Operaciones) {
                        Rol_Operacion asignacionRol = new Rol_Operacion()
                        {
                            idRol = oRol.idRol,
                            idOperacion = oElement.IdOperacion
                        };
                        db.Rol_Operacion.Add(asignacionRol);
                    }
                    db.SaveChanges();
                }
                return Json(null);
            }
            catch (Exception e)
            {

                return Json(e.Message);
            }
        }

        public ActionResult PermisosRol(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolResponse rol = null;
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oRol = db.Rol.Find(id);
                if (oRol == null)
                {
                    return HttpNotFound();
                }
                var oOperacion = db.Rol_Operacion.Where(x => x.idRol == oRol.idRol).ToList();

                rol = new RolResponse()
                {
                    IdRol = oRol.idRol,
                    Nombre = oRol.nombre,
                    Permisos = new List<OperacionesResponse>()
                };
                foreach (var oElements in oOperacion)
                {
                    OperacionesResponse op = new OperacionesResponse()
                    {
                        IdOpeRol = oElements.idRol_Ope,
                        IdOperacion = oElements.idOperacion,
                        IdRol = oElements.idRol
                    };
                    rol.Permisos.Add(op);
                }
            }
            return Json(rol, JsonRequestBehavior.AllowGet);
        }
    

    }
}