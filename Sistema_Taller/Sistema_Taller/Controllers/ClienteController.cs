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
using Sistema_Taller.Models.Request;
using Sistema_Taller.Models.Response;

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

        #region Prueba
        [HttpPost]
        public ActionResult LClientes()
        {

            List<ClienteResponse> clientes = null;

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
                IQueryable<ClienteResponse> query = from d in db.Cliente
                                                    select new ClienteResponse
                                                    {
                                                        IdCliente =d.idCliente,
                                                        Nombre= d.nombre,
                                                        Apellidos = d.apellidos,
                                                        Cedula = d.cedula,
                                                        Telefono = d.telefono,
                                                        Correo = d.correo
                                                    }; 

                if (searchValue.Length > 0)
                {
                    query = query.Where(x => x.Nombre.Contains(searchValue) || x.Apellidos.Contains(searchValue)
                    || x.Cedula.ToString().Contains(searchValue) || x.Correo.Contains(searchValue));
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
        #endregion

        [HttpPost]
        public ActionResult ListaClientes()
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

        public ActionResult Crear()
        {
       
            return View();
        }

        [HttpPost]

        public ActionResult Crear(ClienteRequest model)
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
                        else
                        {
                            ClienteRequest.ProcedureCliente(model, db);
                        }
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
        #region testEditar
        [HttpPost]
        public ActionResult Edit(ClienteResponse model)
        {
            if (ModelState.IsValid)
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oCliente = db.Cliente.Find(model.IdCliente);
                    oCliente.nombre = model.Nombre;
                    oCliente.apellidos = model.Apellidos;
                    oCliente.telefono = model.Telefono;
                    oCliente.correo = model.Correo;
                    db.Entry(oCliente).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Content("1");
            }
            return Content("0");
        }
        #endregion

        #region editar
        [HttpPost]
        public ActionResult Editar(ClienteRequest model)
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
                            dt.Rows.Add(oElement.IdEmpresa, oElement.NombreEmpresa, oElement.CedJuridica, oElement.Direccion, oElement.TelEmpresa, oElement.IdCliente);
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
        #endregion
        #region buscarcliente
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
                cliente.Nombre = oCliente.nombre;
                cliente.Apellidos = oCliente.apellidos;
                cliente.Cedula = oCliente.cedula;
                cliente.Telefono = oCliente.telefono;
                cliente.Correo = oCliente.correo;
                cliente.Negocio = new List<EmpresaViewModel>();

                foreach (var i in oEmpresa)
                {
                    EmpresaViewModel emp = new EmpresaViewModel()
                    {
                        IdEmpresa = i.idEmpresa,
                        NombreEmpresa = i.nombre,
                        CedJuridica = i.cedJuridica,
                        Direccion = i.direccion,
                        TelEmpresa = i.telefono,
                        IdCliente = i.idCliente
                    };
                    cliente.Negocio.Add(emp);
                }

            }

            return Json(cliente,JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region buscarPrueba
        [HttpGet]
        public ActionResult BuscarC(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oCliente = db.Cliente.Find(id);

                if (oCliente == null)
                {
                    return HttpNotFound();
                }
                ClienteResponse cr = new ClienteResponse()
                {
                    IdCliente = oCliente.idCliente,
                    Nombre = oCliente.nombre,
                    Apellidos = oCliente.apellidos,
                    Cedula = oCliente.cedula,
                    Telefono = oCliente.telefono,
                    Correo = oCliente.correo
                };
                return Json(cr, JsonRequestBehavior.AllowGet);
            }
            
        }
        #endregion

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

        [HttpPost]
        public ActionResult Buscar(string term) {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oCliente = db.Cliente.Where(x => x.nombre.Contains(term)
                    || x.cedula.ToString().Contains(term)).Select(d => new
                    {
                        label = d.nombre,
                        id= d.idCliente
                        //cedula = d.cedula
                    }).Take(2).ToList();

                    return Json(oCliente);
                }
            }
            catch (Exception) {
                return Json("0");
            }
        }

    }
}