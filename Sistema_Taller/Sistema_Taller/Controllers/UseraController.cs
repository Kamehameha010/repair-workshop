using Sistema_Taller.Models.ViewModels;
using Sistema_Taller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class UseraController : Controller
    {
        // GET: Usera
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

       
        public JsonResult Lista()
        {
            List<usera> lst = null;
            using (Taller_SysEntities db = new Taller_SysEntities()) {
                lst = db.usera.ToList();
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Crear(UseraViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        
                        usera us = new usera();
                        us.nombre = model.nombre;
                        us.apellidos = model.apellidos;
                        us.cedula = model.cedula;
                        us.telefono = model.telefono;
                        us.correo = model.correo;
                        us.username = model.username;
                        us.contrasena = model.contrasena;
                        db.usera.Add(us);
                        db.SaveChanges();
                    }
                    return Content("1");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
    }
}