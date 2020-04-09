using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_Taller.Models.ViewModels;
using Sistema_Taller.Models;
using System.Net;
using Sistema_Taller.Models.Response;
using System.Linq.Dynamic;

namespace Sistema_Taller.Controllers
{
    public class EmpresaController : Controller
    {
        private string draw = "";
        private string start = "";
        private string length = "";
        private string sortColumn = "";
        private string sortColumnDir = "";
        private string searchValue = "";
        private int pageSize, skip, recordsTotal;
        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ListaEmpresa()
        {

            List<View_Cliente> clientes = null;

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
                IQueryable<View_Cliente> query = db.View_Cliente;

                if (searchValue.Length > 0)
                {
                    query = query.Where(x => x.Nombre.Contains(searchValue) || x.Apellidos.Contains(searchValue)
                    || x.Cedula.ToString().Contains(searchValue) || x.Empresa.Contains(searchValue));
                }
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsTotal = query.Count();

                clientes = query.Skip(skip).Take(pageSize).ToList();
            }
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = clientes });

        }
        [HttpGet]
        public ActionResult BuscarEmpresa(int? id) {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oEmpresa = db.Empresa.Find(id);
                if(oEmpresa == null)
                {
                    return HttpNotFound();
                }
                var oCliente = db.Cliente.Find(oEmpresa.idCliente);
                EmpresaResponse er = new EmpresaResponse()
                {
                    IdCliente = oCliente.idCliente,
                    Nombre = oCliente.nombre,
                    Apellidos = oCliente.apellidos,
                    Cedula = oCliente.cedula,
                    Telefono = oCliente.telefono,
                    Correo = oCliente.correo,
                    IdEmpresa = oEmpresa.idEmpresa,
                    Empresa = oEmpresa.nombre,
                    CedJuridica = oEmpresa.cedJuridica,
                    TelEmpresa = oEmpresa.telefono,
                    Direccion = oEmpresa.direccion
                };

                return Json(er, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Editar(int? id) 
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using(Taller_SysEntities db = new Taller_SysEntities())
            {
                var oEmpresa = db.Empresa.Find(id);
                if (oEmpresa == null) {
                    return HttpNotFound();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Editar(EmpresaResponse model) 
        {
            try
            {
                if (ModelState.IsValid) {

                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        var oEmpresa = db.Empresa.Find(model.IdEmpresa);
                        oEmpresa.nombre = model.Nombre;
                        oEmpresa.cedJuridica = model.CedJuridica;
                        oEmpresa.telefono = model.TelEmpresa;
                        oEmpresa.direccion = model.Direccion;
                        db.Entry(oEmpresa).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Json("1");
                    }
                }
                return View(model);
            }
            catch (Exception) {
                return Json("");
            }
            
        }
        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oEmpresa = db.Empresa.Where(x => x.idEmpresa == id).FirstOrDefault();

                    if(oEmpresa != null)
                    {
                        db.Empresa.Remove(oEmpresa);
                        db.SaveChanges();
                        return Json("1");
                    }
                }
                return Json("No encontrado");
            }catch(Exception e)
            {
                return Json(e.Message);
            }
        }

    }
}