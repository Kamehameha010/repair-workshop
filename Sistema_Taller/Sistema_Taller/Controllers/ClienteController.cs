using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;

namespace Sistema_Taller.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NegocioIndex() 
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

            return View();
        
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
                        Cliente cliente = new Cliente();

                        cliente.nombre = model.Nombre;
                        cliente.apellidos = model.Apellidos;
                        cliente.cedula = model.Cedula;
                        cliente.telefono = model.Telefono;
                        cliente.correo = model.Correo;

                        db.Cliente.Add(cliente);
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
    }
}