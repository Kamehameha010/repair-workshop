using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Agregar()
        {
            return View();
        }

        public static SelectList Marcas()
        {
            
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                return new SelectList(db.Marca.ToList(), "idMarca", "nombre");
            }
        }

        [HttpPost]
        public ActionResult Agregar(MarcaViewModel model)
        {
            try
            {
                if(model != null) {
                    if (ModelState.IsValid)
                    {
                        using (Taller_SysEntities db = new Taller_SysEntities())
                        {
                            Marca oMarca = new Marca()
                            {
                                nombre = model.Nombre,
                                descripcion = model.Descripcion
                            };
                            db.Marca.Add(oMarca);
                            db.SaveChanges();
                        }

                        return Content("1");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult Editar(int? id)
        {

            if(id == null)
            {
                return HttpNotFound("Registro no encontrado");
            }
            MarcaViewModel oMarca = new MarcaViewModel();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var marca = db.Marca.Find(id);
                oMarca.IdMarca = marca.idMarca;
                oMarca.Nombre = marca.nombre;
                oMarca.Descripcion = marca.descripcion;
            }

            return Json(oMarca);
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                
                using (Taller_SysEntities db = new Taller_SysEntities()) {
                    var oMarca = db.Marca.Find(id);
                    return Json(oMarca);
                }
            }
            else {
                return HttpNotFound();
            }
        }

        public ActionResult Eliminar(int id) {

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oMarca = db.Marca.Find(id);
                db.Marca.Remove(oMarca);
                db.SaveChanges();
            }
            return Content("1");
        }

    }
}