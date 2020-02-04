using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoría
        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }


        public static SelectList Categorias()
        {
            
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                return new SelectList(db.Categoria.ToList(), "idCategoria", "nombre");
            }
            
        }

        [HttpPost]
        public ActionResult Crear(CategoriaViewModel model)
        {
            try
            {
                if(model == null)
                {
                    throw new NullReferenceException();
                }

                if(ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities()) {

                        Categoria OCategoria = new Categoria()
                        {
                            nombre = model.Nombre,
                            descrip = model.Descripcion
                        };
                        db.Categoria.Add(OCategoria);
                        db.SaveChanges();

                    }
                    return Content("1");
                }
                return View(model);
            }
            catch(DbUpdateException ex )
            {
                return Content(ex.Message);
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
                return HttpNotFound();
            }
            CategoriaViewModel oCategoria = new CategoriaViewModel();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var categoria = db.Categoria.Find(id);
                oCategoria.IdCategoria = categoria.idCategoria;
                oCategoria.Nombre = categoria.nombre;
                oCategoria.Descripcion = categoria.descrip;
            }
            return Json(oCategoria);
        }

        [HttpPost]
        public ActionResult Editar(CategoriaViewModel model)
        {
            try
            {
                if(model != null) {
                    if (ModelState.IsValid)
                    {
                        using (Taller_SysEntities db = new Taller_SysEntities())
                        {

                            var oCategoria = db.Categoria.Find(model.IdCategoria);
                            oCategoria.nombre = model.Nombre;
                            oCategoria.descrip = model.Descripcion;
                            db.Entry(oCategoria).State = System.Data.Entity.EntityState.Modified;
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
        public ActionResult Eliminar(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            CategoriaViewModel categoria = new CategoriaViewModel();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oCategoria = db.Categoria.Find(id);
                categoria.IdCategoria = oCategoria.idCategoria;
                categoria.Nombre = oCategoria.nombre;
                categoria.Descripcion = oCategoria.descrip;
            }
            return Json(categoria, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oCategoria = db.Categoria.Find(id);
                db.Categoria.Remove(oCategoria);
                db.SaveChanges();
            }
            return Content("1");
        }
    }
}