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
            return View();
        }


        public JsonResult ListaUsuarios()
        {
            List<View_Usuario> usuarios = new List<View_Usuario>();

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                usuarios = db.View_Usuario.ToList();

            }
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            ViewBag.estados = ListaComboBox.estados();
            ViewBag.roles = ListaComboBox.roles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Prueba(PruebaV model) 
        {
            ViewBag.nombre = model.nombre;
            return Json(true);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(UsuarioViewModel model)
        {

            ViewBag.estados = ListaComboBox.estados();
            ViewBag.roles = ListaComboBox.roles();
            try
            {
                if (ModelState.IsValid && model!= null)
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
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
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

        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioViewModel model)
        {
            try
            {
                ViewBag.estados = ListaComboBox.estados();
                ViewBag.roles = ListaComboBox.roles();
                using (Taller_SysEntities db = new Taller_SysEntities())
                {

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
                return Content("1");
            }
            catch (Exception e)
            {
                return Content(e.Message); ;
            }



        }

        public ActionResult Eliminar(int id)
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
        [HttpPost]
        public ActionResult ConfirmarEliminar(int id)
        {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var oUsuario = db.Usuario.Find(id);
                    db.Usuario.Remove(oUsuario);
                    db.SaveChanges();
                }
                return Content("1");
            }catch(Exception e)
            {
                return Content(e.Message);
            }
            
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
                return Content("0");
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

        public ActionResult Modificar(int? id)
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
        public ActionResult Cambio(UsuarioViewModel model)
        {
            try
            {
                ViewBag.estados = ListaComboBox.estados();
                ViewBag.roles = ListaComboBox.roles();
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {

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
                    return Content("1");
                }
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message); ;
            }


        }

        [HttpPost]
        public ActionResult Borrar(int id) {

            using (Taller_SysEntities db = new Taller_SysEntities()) {

                var obj = db.Usuario.Find(id);

                db.Usuario.Remove(obj);
                db.SaveChanges();
            
            }
            return Content("1");
        }
    }
}