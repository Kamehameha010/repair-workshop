using System.Web.Mvc;
using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Sistema_Taller.Controllers
{
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
            List<Estado> est = new List<Estado>();
            List<SelectListItem> roles = null;
            using (Taller_SysEntities db = new Taller_SysEntities())
            {

                est = db.Estado.ToList();
                roles = db.Rol.ToList().ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.nombre,
                        Value = d.idRol.ToString(),
                        Selected = false
                    };
                });
            }
            List<SelectListItem> estados = est.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion,
                    Value = d.idEstado.ToString(),
                    Selected = false
                };
            });

            ViewBag.estados = estados;
            ViewBag.Roles = roles;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(UsuarioViewModel model)
        {
            if(ModelState.IsValid)
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    Models.Usuario usuario = new Models.Usuario();
                    usuario.nombre = model.nombre;
                    usuario.apellidos = model.apellidos;
                    usuario.cedula = model.cedula;
                    usuario.telefono = model.telefono;
                    usuario.username = model.username;
                    usuario.contrasena = model.contrasena;
                    usuario.idRol = model.idRol;
                    usuario.idEstado = model.idEstado;
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Editar(int? id)
        {
            UsuarioViewModel model = new UsuarioViewModel();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                var objUsuario = db.Usuario.Find(id);
                model.nombre = objUsuario.nombre;
                model.apellidos = objUsuario.apellidos;
                model.cedula = objUsuario.cedula;
                model.telefono = objUsuario.telefono;
                model.username = objUsuario.username;
                model.correo = objUsuario.correo;
                model.idEstado = objUsuario.idEstado;
                model.idRol = objUsuario.idRol;

            }
            return View(model);
        }

        [HttpPut,ActionName("Editar")]
        public ActionResult Editar(UsuarioViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        Usuario usuario = new Usuario();

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

                        return RedirectToAction("Index");

                    }

                }
            }catch(System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

            return View(model);
        }
        public ActionResult Eliminar(int? id)
        {
            Usuario model = new Usuario();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                model  = db.Usuario.Find(id);
            }
            return View(model);
        }

        [HttpDelete,ActionName("Eliminar")]
        public ActionResult ConfirmarEliminar(int id)
        {

            return View();
        }
    }
}