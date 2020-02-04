using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace Sistema_Taller.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult ListadoClientes() {

            List<View_Cliente> clientes = null;

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                clientes = db.View_Cliente.ToList(); 
            }
            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListadoNegocios()
        {
            List<View_Negocio> negocios = null;

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                negocios = db.View_Negocio.ToList();
            }
            return Json(negocios, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Crear() {

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
                        var dt = new DataTable();
                        dt.Columns.Add("nombre",typeof(string));
                        dt.Columns.Add("cedJuridica", typeof(string));
                        dt.Columns.Add("direccion",typeof(string));
                        dt.Columns.Add("telefono",typeof(string));
                        dt.Columns.Add("idCliente",typeof(int));

                        int i = 1;
                        foreach (var oElement in model.Empresa)
                        {
                            dt.Rows.Add(oElement.Nombre, oElement.CedJuridica, oElement.Direccion, oElement.Telefono, i);
                            i++;
                        }

                        var parametros = new SqlParameter("@Negocio", SqlDbType.Structured);
                        parametros.Value = dt;
                        parametros.TypeName = "dbo.Negocios";

                        
                        db.Database.ExecuteSqlCommand("exec Sp_AddCliente @nombre, @apellidos,@cedula,@telefono,@correo, @Negocio"
                            , new SqlParameter("@nombre",model.Nombre),
                            new SqlParameter("@apellidos",model.Apellidos),
                            new SqlParameter("@cedula",model.Cedula),
                            new SqlParameter("@telefono",model.Telefono),
                            new SqlParameter("@correo",model.Correo),parametros);

                        /*
                        Cliente cliente = new Cliente()
                        {

                            nombre = model.Nombre,
                            apellidos = model.Apellidos,
                            cedula = model.Cedula,
                            telefono = model.Telefono,
                            correo = model.Correo
                        };
                        db.Cliente.Add(cliente);
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
                                    direccion = oEmpresa.Direccion,
                                    idCliente = cliente.idCliente
                                };
                                db.Empresa.Add(empresa);
                            }

                            db.SaveChanges();
                        }*/
                    }
                    return Content("1");
                }

            }catch(Exception e)
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
            }catch(Exception ex)
            {
                ViewBag.Error = "Ha ocurrido un problema. Intente de nuevo!";
                return Content(ex.Message);
            }
            return View(model);
        }

 
        public ActionResult Editar(int id)
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
       // [ValidateAntiForgeryToken]
        public ActionResult Update(ClienteViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
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
                
            }catch(Exception e)
            {
                return Content(e.Message);
            }
            return View(model);
        }
   
        public ActionResult Eliminar(int id)
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
        public ActionResult Borrar(int id)
        {
            
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oCliente = db.Cliente.Find(id);
                db.Cliente.Remove(oCliente);
                db.SaveChanges();
            }

            return Content("1");
        }
        [HttpPost]
        public JsonResult Dele(int id)
        {
            using ( Taller_SysEntities db = new Taller_SysEntities())
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