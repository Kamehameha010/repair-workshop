using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using Sistema_Taller.Models.ViewModels.BuscarViewModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Dynamic;
using System.Net;
using System.Data.Entity;

namespace Sistema_Taller.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente

        private string draw = "";
        private string start = "";
        private string length = "";
        private string sortColumn = "";
        private string sortColumnDir = "";
        private string searchValue = "";
        private int pageSize, skip, recordsTotal;
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult ListaClientes()
         {

            List<View_Cliente> clientes = null;
            List<View_Caso> lst = new List<View_Caso>();
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
        public JsonResult Lista()
        {

            List<View_Cliente> clientes = null;

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                clientes = db.View_Cliente.ToList();
            }
            return Json(clientes ,JsonRequestBehavior.AllowGet);

        }

        public ActionResult Crear()
        {

            ClienteViewModel model = new ClienteViewModel()
            {
                Empresa = new List<EmpresaViewModel>()
            };

            return View(model);
        }

        [HttpPost]

        public ActionResult Add(ClienteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        if (model.Empresa == null)
                        {
                            Cliente ocliente = new Cliente()
                            {
                                nombre = model.Nombre,
                                apellidos = model.Apellidos,
                                cedula = model.Cedula,
                                telefono = model.Telefono,
                                correo = model.Correo
                            };

                            db.Cliente.Add(ocliente);
                            db.SaveChanges();
                        
                        }

                        if (model.Empresa != null)
                        {
                            var dt = new DataTable();
                            dt.Columns.Add("id", typeof(int));
                            dt.Columns.Add("nombre", typeof(string));
                            dt.Columns.Add("cedJuridica", typeof(string));
                            dt.Columns.Add("direccion", typeof(string));
                            dt.Columns.Add("telefono", typeof(string));
                            dt.Columns.Add("idCliente", typeof(int));

                            int i = 1;
                            foreach (var oElement in model.Empresa)
                            {
                                dt.Rows.Add(i, oElement.Nombre, oElement.CedJuridica, oElement.Direccion, oElement.Telefono, i);
                            }

                            var parametros = new SqlParameter("@Negocio", SqlDbType.Structured)
                            {
                                Value = dt,
                                TypeName = "dbo.typ_negocio"
                            };


                             db.Database.ExecuteSqlCommand("exec Sp_AddCliente @nombre, @apellidos,@cedula,@telefono,@correo, @Negocio"
                                , new SqlParameter("@nombre", model.Nombre),
                                new SqlParameter("@apellidos", model.Apellidos),
                                new SqlParameter("@cedula", model.Cedula),
                                new SqlParameter("@telefono", model.Telefono),
                                new SqlParameter("@correo", model.Correo), parametros);

                        }
                    }
                    return Content("1");
                }

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return View(model);
        }

        public ActionResult AddEmpresa(ClienteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        Cliente oCliente = new Cliente()
                        {
                            nombre = model.Nombre,
                            apellidos = model.Apellidos,
                            cedula = model.Cedula,
                            telefono = model.Telefono,
                            correo = model.Correo
                        };
                        db.Cliente.Add(oCliente);
                        db.SaveChanges();

                        if (model.Empresa != null)
                        {
                            foreach (var oEmpresa in model.Empresa)
                            {
                                Empresa empresa = new Empresa()
                                {
                                    nombre = oEmpresa.Nombre,
                                    cedJuridica = oEmpresa.CedJuridica,
                                    telefono = oEmpresa.Telefono,
                                    direccion = oEmpresa.Direccion
                                };
                                db.Empresa.Add(empresa);
                            }

                            db.SaveChanges();
                        }
                    }
                    ViewBag.message = "Registro ingresado correctamente";
                    return Content("1");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un problema. Intente de nuevo!";
                return Content(ex.Message);
            }
            return View(model);
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
                var oCliente = db.Cliente.Find(id);
                if(oCliente == null)
                {
                    return HttpNotFound();
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Editar(ClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    if (model.Empresa == null)
                    {
                        var oCliente = db.Cliente.Find(model.IdCliente);
                        oCliente.nombre = model.Nombre;
                        oCliente.apellidos = model.Apellidos;
                        oCliente.cedula = model.Cedula;
                        oCliente.telefono = model.Telefono;
                        oCliente.correo = model.Correo;
                    }
                    else
                    {
                        var dt = new DataTable();
                        dt.Columns.Add("id", typeof(int));
                        dt.Columns.Add("nombre", typeof(string));
                        dt.Columns.Add("cedJuridica", typeof(string));
                        dt.Columns.Add("direccion", typeof(string));
                        dt.Columns.Add("telefono", typeof(string));
                        dt.Columns.Add("idCliente", typeof(int));

                        int i = 1;
                        foreach (var oElement in model.Empresa)
                        {
                            dt.Rows.Add(oElement.IdEmpresa, oElement.Nombre, oElement.CedJuridica, oElement.Direccion, oElement.Telefono, oElement.IdCliente);
                            i++;
                        }

                        var parametros = new SqlParameter("@Negocio", SqlDbType.Structured)
                        {
                            Value = dt,
                            TypeName = "dbo.typ_negocio"
                        };


                        db.Database.ExecuteSqlCommand("exec sp_ActCliente @idCliente, @nombre, @apellidos,@cedula,@telefono,@correo, @Negocio"
                            , new SqlParameter("@idCliente", model.IdCliente),
                            new SqlParameter("@nombre", model.Nombre),
                            new SqlParameter("@apellidos", model.Apellidos),
                            new SqlParameter("@cedula", model.Cedula),
                            new SqlParameter("@telefono", model.Telefono),
                            new SqlParameter("@correo", model.Correo), parametros);

                    }
                }
                return Content("1");
            }
            return Content("0");
        }

        [HttpGet]
        public ActionResult BuscarCliente(int? id)
        {
            BuscarCliente cliente = new BuscarCliente();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var oEmpresa = db.Empresa.Where(i => i.idCliente == id).ToList();
                var oCliente = db.Cliente.Find(id);
                cliente.IdCliente = oCliente.idCliente;
                cliente.Nombre = oCliente.nombre;
                cliente.Apellidos = oCliente.apellidos;
                cliente.Cedula = oCliente.cedula;
                cliente.Telefono = oCliente.telefono;
                cliente.Correo = oCliente.correo;
                cliente.Negocio = new List<EmpresaViewModel>();

                foreach (var i in oEmpresa)
                {
                    EmpresaViewModel emp = new EmpresaViewModel();
                    emp.IdEmpresa = i.idEmpresa;
                    emp.Nombre = i.nombre;
                    emp.CedJuridica = i.cedJuridica;
                    emp.Direccion = i.direccion;
                    emp.Telefono = i.telefono;
                    emp.IdCliente = i.idCliente;
                    cliente.Negocio.Add(emp);
                }

            }

            return Json(cliente,JsonRequestBehavior.AllowGet);
        }
 

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Update(ClienteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {

                        var oCliente = db.Cliente.Find(model.IdCliente);
                        //cliente.idCliente = model.IdCliente;
                        oCliente.nombre = model.Nombre;
                        oCliente.apellidos = model.Apellidos;
                        oCliente.cedula = model.Cedula;
                        oCliente.telefono = model.Telefono;
                        oCliente.correo = model.Correo;

                        db.Entry(oCliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Content("1");
                }

            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Eliminar(int? id)
        {
            ClienteViewModel cliente = new ClienteViewModel();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oCliente = db.Cliente.Find(id);
                cliente.IdCliente = oCliente.idCliente;
                cliente.Nombre = oCliente.nombre;
                cliente.Apellidos = oCliente.apellidos;
                cliente.Cedula = oCliente.cedula;
                cliente.Telefono = oCliente.telefono;
                cliente.Correo = oCliente.correo;
            }
            return View(cliente);
        }
        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oCliente = db.Cliente.Find(id);
                if (oCliente == null)
                {
                    return HttpNotFound();
                }
                else {
                    db.Cliente.Remove(oCliente);
                    db.SaveChanges();
                }
            }
            return Content("1");
        }
        [HttpPost]
        public JsonResult Dele(int id)
        {
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var obj = db.Cliente.Find(id);

                db.Cliente.Remove(obj);
                db.SaveChanges();
            }
            return Json(true);
        }

        public ActionResult Prueba()
        {
            return View();

        }

        [HttpPost]
        public JsonResult Reporte(int id)
        {
            ClienteViewModel c = new ClienteViewModel();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oC = db.Cliente.Find(id);

                c.IdCliente = oC.idCliente;
                c.Nombre = oC.nombre;
                c.Apellidos = oC.apellidos;
                c.Cedula = oC.cedula;
                c.Telefono = oC.telefono;
                c.Correo = oC.correo;
            }
            return Json(c);
        }

    }
}