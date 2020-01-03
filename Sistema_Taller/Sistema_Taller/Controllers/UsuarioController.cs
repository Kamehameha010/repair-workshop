using System.Web.Mvc;
using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System;
namespace Sistema_Taller.Controllers
{
    [Route("Usuario")]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List<View_Usuario> usuarios = new List<View_Usuario>();

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                usuarios = db.View_Usuario.ToList();

            }

            return View(usuarios);
        }
        public ActionResult Crear()
        {
            ViewBag.estados = ListaComboBox.estados();
            ViewBag.roles = ListaComboBox.roles();
            return View();
        }


        [HttpPost]
        [Route("Usuario/Crear")]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {

                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    try
                    {
                        Models.Usuario usuario = new Models.Usuario();
                        usuario.nombre = model.nombre;
                        usuario.apellidos = model.apellidos;
                        usuario.cedula = model.cedula;
                        usuario.telefono = model.telefono;
                        usuario.correo = model.correo;
                        usuario.username = model.username;
                        usuario.contrasena = model.contrasena;
                        usuario.idRol = model.idRol;
                        usuario.idEstado = model.idEstado;
                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateException e)
                    {
                        SqlException s = e.InnerException.InnerException as SqlException;
                        if (s != null && s.Number == 2627)
                        {
                            ModelState.AddModelError(string.Empty,
                                "Part number '" + model.cedula + "' already exists.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty,
                                "An error occured - please contact your system administrator.");
                        }
                    }

                }
            }
            ViewBag.estados = ListaComboBox.estados();
            ViewBag.roles = ListaComboBox.roles();

            return View(model);
        }

        public ActionResult Editar(int? id)
        {
            UsuarioViewModel model = new UsuarioViewModel();

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var objUsuario = db.Usuario.Find(id);
                model.idUsuario = objUsuario.idUsuario;
                model.nombre = objUsuario.nombre;
                model.apellidos = objUsuario.apellidos;
                model.cedula = objUsuario.cedula;
                model.telefono = objUsuario.telefono;
                model.username = objUsuario.username;
                model.contrasena = objUsuario.contrasena;
                model.correo = objUsuario.correo;
                model.idEstado = objUsuario.idEstado;
                model.idRol = objUsuario.idRol;

            }
            ViewBag.estados = ListaComboBox.estados();
            ViewBag.roles = ListaComboBox.roles(); ;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioViewModel model)
        {
            SelectList roles = null;
            SelectList estados = null;

            if (ModelState.IsValid)
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    estados = new SelectList(db.Estado.ToList(), "idEstado", "descripcion");
                    roles = new SelectList(db.Rol.ToList(), "idRol", "nombre");
                    var usuario = db.Usuario.Find(model.idUsuario);
                    usuario.nombre = model.nombre;
                    usuario.apellidos = model.apellidos;
                    usuario.cedula = model.cedula;
                    usuario.telefono = model.telefono;
                    usuario.username = model.username;
                    usuario.correo = model.correo;
                    usuario.contrasena = model.contrasena;
                    usuario.idRol = model.idRol;
                    usuario.idEstado = model.idEstado;

                    db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.estados = estados;
            ViewBag.roles = roles;
            return View(model);
        }

        public ActionResult Eliminar(int? id)
        {
            Usuario model = new Usuario();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                model = db.Usuario.Find(id);
            }
            ViewBag.sms = "Lista de objetos";
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(int id)
        {
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var oUsuario = db.Usuario.Find(id);
                db.Usuario.Remove(oUsuario);
                db.SaveChanges();
            }
            return View();
        }
        [HttpGet]
        [Route("Usuario/ValidarCedula")]
        public ActionResult ValidarCedula(int? cedula)
        {

            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var fnd = db.Usuario.Where(s => s.cedula == cedula).Select(s => s.cedula).First();
                }
                return Content("Elemento Existe");
            }
            catch (Exception)
            {
                return Content("1");
            }
        }

        [HttpGet]
        [Route("Usuario/ValidarUsuario")]
        public ActionResult ValidarUsuario(string usuario)
        {

            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var fnd = db.Usuario.Where(s => s.username == usuario).Select(s => s.username).First();
                }
                return Content("Elemento Existe");
            }
            catch (Exception)
            {
                return Content("1");
            }
        }

    }

}