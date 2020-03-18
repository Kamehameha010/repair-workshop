
using Sistema_Taller.Models;
using Sistema_Taller.Models.Request;
using Sistema_Taller.Models.Response;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace Sistema_Taller.Controllers
{
    public class RepuestoController : Controller
    {

        private string draw = "";
        private string start = "";
        private string length = "";
        private string sortColumn = "";
        private string sortColumnDir = "";
        private string searchValue = "";
        private int pageSize, skip, recordsTotal;
        // GET: Repuesto
        public ActionResult Inventario()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Stock()
        {
            List<View_Repuesto> lst = new List<View_Repuesto>();
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
                IQueryable<View_Repuesto> query = db.View_Repuesto;

                if (searchValue.Length > 0)
                {
                    query = query.Where(x => x.codigo.Contains(searchValue) || x.descripcion.Contains(searchValue) 
                    || x.precio.ToString().Contains(searchValue) || x.cantidad.ToString().Contains(searchValue));
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
        public JsonResult Proveedores(string nombre)
        {

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var res = db.ProveedorRepuesto.Where(x => x.nombre.Contains(nombre))
                    .Select(x => x.nombre)
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
        public ActionResult Crear(RespuestoRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        var dt = new DataTable();
                        dt.Columns.Add("id", typeof(int));
                        dt.Columns.Add("idProveedor", typeof(int));
                        dt.Columns.Add("codigo", typeof(string));
                        dt.Columns.Add("descripcion", typeof(string));
                        dt.Columns.Add("precio", typeof(decimal));
                        dt.Columns.Add("cantidad", typeof(int));

                        int fila = 0;
                        foreach (var oRepuesto in model.Repuesto)
                        {
                            dt.Rows.Add(fila, model.idProveedor, oRepuesto.codigo, oRepuesto.descripcion, oRepuesto.precio, oRepuesto.cantidad);
                        }

                        var parametros = new SqlParameter("@repuesto", SqlDbType.Structured)
                        {
                            Value = dt,
                            TypeName = "dbo.typ_repuesto"
                        };

                        db.Database.ExecuteSqlCommand("exec Sp_AddRepuesto @id, @proveedor, @telefono, @direccion, @repuesto",
                            new SqlParameter("@id", model.idProveedor),
                            new SqlParameter("@proveedor", model.nombre),
                            new SqlParameter("@telefono", model.telefono),
                            new SqlParameter("@direccion", model.direccion),
                            parametros
                            );

                    }
                    return Json("1");
                }
                return View(model);
            }
            catch (Exception e)
            {

                return Json("0");
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
                var oEncontrar = db.InventarioRepuesto.Find(id);
                if (oEncontrar == null)
                {
                    return HttpNotFound();
                }
            }
            return View();
        }

        public ActionResult Editar(RepuestoViewModel model)
        {
            
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oEncontrar = db.InventarioRepuesto.Find(model.Repuestos.idInvRep);
                
            }
            return View();
        }

        [HttpGet]
        public ActionResult BuscarProveedorRepuesto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepuestoResponse oElemento = null;

            using (Taller_SysEntities db = new Taller_SysEntities())
            {

                var oEncontrar = db.InventarioRepuesto.Find(id);
                if (oEncontrar == null)
                {
                    return HttpNotFound();
                }
                var oProveedor = db.ProveedorRepuesto.Find(oEncontrar.idProveedor);

                oElemento = new RepuestoResponse()
                {
                    idProveedor  =oProveedor.idProveedor,
                    nombre = oProveedor.nombre,
                    telefono = oProveedor.telefono,
                    direccion = oProveedor.direccion,
                    idInventario = oEncontrar.idInvRep,
                    Codigo = oEncontrar.codigo,
                    Descripcion = oEncontrar.descripcion,
                    Precio = oEncontrar.precio,
                    Cantidad = oEncontrar.cantidad

                };
            }
            return Json(oElemento, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oElement = db.InventarioRepuesto.Find(id);
                    db.InventarioRepuesto.Remove(oElement);
                    db.SaveChanges(); 
                }
                return Json("1");
            }catch(Exception e)
            {
                return Json("Error");
            }
        }

        [HttpPost]
        public JsonResult EditarProveedor(ProveedorRepuesto model)
        {
            if (ModelState.IsValid)
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oProveedor = db.ProveedorRepuesto.Find(model.idProveedor);

                    oProveedor.nombre = model.nombre;
                    oProveedor.telefono = model.telefono;
                    oProveedor.direccion = model.direccion;

                    db.Entry(oProveedor).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();    

                }
                return Json("1");
            }
            return Json("0");
        }

        [HttpPost]
        public JsonResult EditarRepuesto(InventarioRepuesto model)
        {
            if (ModelState.IsValid)
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oRepuesto = db.InventarioRepuesto.Find(model.idInvRep);

                    oRepuesto.codigo = model.codigo;
                    oRepuesto.precio = model.precio;
                    oRepuesto.cantidad = model.cantidad;
                    oRepuesto.descripcion = model.descripcion;

                    db.Entry(oRepuesto).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
                return Json("1");
            }
            return Json("0");
        }
    }
}