using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class ArticuloController : Controller
    {
        // GET: Articulo
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Crear()
        {
            ViewBag.categorias = CategoriaController.Categorias();
            ViewBag.marcas = MarcaController.Marcas();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ArticuloViewModel model) {
            try
            {
                ViewBag.categorias = CategoriaController.Categorias();
                ViewBag.marcas = MarcaController.Marcas();
                if (model != null)
                {
                    if (ModelState.IsValid)
                    {
                        using (Taller_SysEntities db = new Taller_SysEntities())
                        {
                            Articulo articulo = new Articulo()
                            {
                                nombre = model.Nombre,
                                modelo = model.Modelo,
                                codigo = model.Codigo,
                                idMarca = model.IdMarca,
                                idCategoria = model.IdCategoria,
                                serie = model.Serie,
                            };
                            db.Articulo.Add(articulo);
                            db.SaveChanges();
                        }
                        return Content("1");
                    }
                }
                return View(model);

            }catch(Exception ex )
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            try { 

                if(id == null)
                {
                    return HttpNotFound();
                }

                ArticuloViewModel oArticulo = new ArticuloViewModel();

                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var articulo = db.Articulo.Find(id);
                    oArticulo.IdArticulo = articulo.idArticulo;
                    oArticulo.Nombre = articulo.nombre;
                    oArticulo.Modelo = articulo.modelo;
                    oArticulo.Codigo = articulo.codigo;
                    oArticulo.IdMarca = articulo.idMarca;
                    oArticulo.IdCategoria = articulo.idCategoria;
                    oArticulo.Serie = articulo.serie;
                    
                }
                return Json(oArticulo);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }

        }

        [HttpPost]

        public ActionResult Editar(ArticuloViewModel model)
        {
            if( ModelState.IsValid)
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oArticulo = db.Articulo.Find(model.IdArticulo);
                    oArticulo.nombre = model.Nombre;
                    oArticulo.codigo = model.Codigo;
                    oArticulo.modelo = model.Modelo;
                    oArticulo.idMarca = model.IdMarca;
                    oArticulo.idCategoria = model.IdCategoria;
                    oArticulo.serie = model.Serie;

                    db.Entry(oArticulo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Content("1");
            }
            return View(model);
        }

        [HttpGet ]
        public ActionResult Eliminar(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oArticulo = db.Articulo.Find(id);
                return Json(oArticulo);
            }

        }
        [HttpPost]
        public ActionResult Eliminar(int id) {
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oAticulo = db.Articulo.Find(id);
                db.Articulo.Remove(oAticulo);
                db.SaveChanges();
            }
            return Content("1");
        }
    }
}