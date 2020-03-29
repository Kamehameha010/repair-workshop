using System.Web.Mvc;
using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System;
using Sistema_Taller.Filtro;
using System.Net;
using Sistema_Taller.Models.Response;
using Sistema_Taller.Models.Request;

namespace Sistema_Taller.Controllers
{

    public class UsuarioController : Controller
    {
        // GET: Usuario
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
        public JsonResult ListaUsuarios()
        {
            List<View_Usuario> usuarios = new List<View_Usuario>();

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
                IQueryable<View_Usuario> query = db.View_Usuario;

                if (searchValue.Length > 0)
                {
                    query = query.Where(x => x.nombre.Contains(searchValue) || x.apellidos.Contains(searchValue)
                    || x.Usuario.Contains(searchValue) || x.cedula.ToString().Contains(searchValue));
                }
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsTotal = query.Count();

                usuarios = query.Skip(skip).Take(pageSize).ToList();
            }
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = usuarios });
        }

        [HttpGet]
        [AuthorizedUser(idOperacion: 1)]
        public ActionResult Crear()
        {
            ViewBag.estados = ListaComboBox.estados();
            ViewBag.roles = ListaComboBox.roles();
            return View();
        }

        [HttpPost]

        public ActionResult Crear(UsuarioViewModel model)
        {

            ViewBag.estados = ListaComboBox.estados();
            ViewBag.roles = ListaComboBox.roles();
            try
            {
                if (ModelState.IsValid && model != null)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {

                        Models.Usuario usuario = new Models.Usuario()
                        {
                            nombre = model.nombre,
                            apellidos = model.apellidos,
                            cedula = model.cedula,
                            telefono = model.telefono,
                            correo = model.correo,
                            username = model.username,
                            contrasena = model.contrasena,
                            idRol = model.idRol,
                            idEstado = model.idEstado,
                        };
                        db.Usuario.Add(usuario);
                        db.SaveChanges();


                    }
                    return Content("1");
                }
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        [AuthorizedUser(idOperacion: 2)]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oUsuario = db.Usuario.Find(id);
                if (oUsuario == null)
                {
                    return HttpNotFound();
                }
            }
            ViewBag.estados = ListaComboBox.estados();
            ViewBag.roles = ListaComboBox.roles(); ;
            return View();
        }

        [HttpPost]
        public ActionResult Editar(UsuarioRequest model)
        {
            try
            {
                ViewBag.estados = ListaComboBox.estados();
                ViewBag.roles = ListaComboBox.roles(); ;
                if (ModelState.IsValid)
                {

                    ViewBag.estados = ListaComboBox.estados();
                    ViewBag.roles = ListaComboBox.roles();
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {

                        var usuario = db.Usuario.Find(model.idUsuario);
                        usuario.nombre = model.nombre;
                        usuario.apellidos = model.apellidos;
                        if (!string.IsNullOrEmpty(model.contrasena))
                        {
                            usuario.contrasena = model.contrasena;
                        }
                        usuario.telefono = model.telefono;
                        usuario.username = model.username;
                        usuario.correo = model.correo;
                        usuario.idRol = model.idRol;
                        usuario.idEstado = model.idEstado;

                        db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Content("1");
                }
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message); ;
            }
        }
        [HttpGet]
        public ActionResult BuscarUsuario(int? id) {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oUsuario = db.Usuario.Find(id);
                if (oUsuario == null)
                {
                    return HttpNotFound();
                }
                UsuarioResponse ur = new UsuarioResponse()
                {
                    idUsuario = oUsuario.idUsuario,
                    nombre = oUsuario.nombre,
                    apellidos = oUsuario.apellidos,
                    cedula = oUsuario.cedula,
                    telefono = oUsuario.telefono,
                    correo = oUsuario.correo,
                    username = oUsuario.username,
                    idEstado = oUsuario.idEstado,
                    idRol = oUsuario.idRol
                };

                return Json(ur, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oUsuario = db.Usuario.Find(id);
                    if (oUsuario == null)
                    {
                        return HttpNotFound();
                    }
                    db.Usuario.Remove(oUsuario);
                    db.SaveChanges();
                    
                }
                return Json("1");
            }
            catch ( Exception e)
            {
                return Json("0");
            }
            
        }
      
        [HttpPost]
        public ActionResult ValidarCedula(int? cedula)
       {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var fnd = db.Usuario.Where(s => s.cedula == cedula).ToList();
                    if(fnd.Count > 0)
                    {
                        return Json("0");
                    }
                    return Json("1");
                }
            }
            catch (Exception)
            {
                return Json("1");
            }
        }

        [HttpPost]
        public ActionResult ValidarUsuario(string usuario)
        {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var fnd = db.Usuario.Where(s => s.username == usuario).ToList();
                    if (fnd.Count > 0)
                    {
                        return Content("0");
                    }
                    return Content("1");
                }     
            }
            catch (Exception)
            {
                return Content("1");
            }
        }


    }
}