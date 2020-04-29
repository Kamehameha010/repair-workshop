using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class ProveedorController : Controller
    {
        private string draw = "";
        private string start = "";
        private string length = "";
        private string sortColumn = "";
        private string sortColumnDir = "";
        private string searchValue = "";
        private int pageSize, skip, recordsTotal;
        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ListaProveedores()
        {
            List<ProveedorViewModel> lst = new List<ProveedorViewModel>();
            draw = Request.Form.GetValues("draw").FirstOrDefault();
            start = Request.Form.GetValues("start").FirstOrDefault();
            length = Request.Form.GetValues("length").FirstOrDefault();
            sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                IQueryable<ProveedorViewModel> query = db.ProveedorRepuesto
                    .Select( z=> new ProveedorViewModel
                    {
                        IdProveedor = z.idProveedor,
                        Nombre = z.nombre,
                        Correo = z.correo,
                        Telefono = z.telefono,
                        Direccion = z.direccion
                    });

                if (searchValue.Length > 0)
                {
                    query = query.Where(x => x.Nombre.Contains(searchValue) || x.Direccion.Contains(searchValue)
                    || x.Correo.Contains(searchValue));
                }
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsTotal = query.Count();

                lst = query.Skip(skip).Take(pageSize).ToList();
            }
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = lst });

        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(ProveedorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        ProveedorRepuesto pr = new ProveedorRepuesto()
                        {
                            nombre = model.Nombre,
                            correo = model.Correo,
                            telefono = model.Telefono,
                            direccion = model.Direccion
                        };

                        db.ProveedorRepuesto.Add(pr);
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
        public ActionResult Editar(ProveedorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        var oProveedor =db.ProveedorRepuesto.Find(model.IdProveedor);

                        oProveedor.nombre = model.Nombre;
                        oProveedor.correo = model.Correo;
                        oProveedor.telefono = model.Telefono;
                        oProveedor.direccion = model.Direccion;
                        
                        db.Entry(oProveedor).State = System.Data.Entity.EntityState.Modified;
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
            try
            {


                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oProveedor = db.ProveedorRepuesto.Find(id);
                    db.ProveedorRepuesto.Remove(oProveedor);
                    db.SaveChanges();
                    return Json("1");
                }
            }catch(Exception)
            {
                return Json("0");
            }
            
        }

        [HttpGet]
        public ActionResult BuscarProveedor(int? id) { 

            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oElement = db.ProveedorRepuesto.Find(id);

                if(oElement == null)
                {
                    return HttpNotFound();
                }

                var oProveedor = new ProveedorViewModel()
                {
                    IdProveedor = oElement.idProveedor,
                    Nombre = oElement.nombre,
                    Correo = oElement.correo,
                    Telefono = oElement.telefono,
                    Direccion = oElement.direccion
                };
                return Json(oProveedor, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public ActionResult EncontrarProveedor(string term)
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


