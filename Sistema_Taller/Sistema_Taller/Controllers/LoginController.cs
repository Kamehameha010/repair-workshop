using Sistema_Taller.Models;
using Sistema_Taller.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginRequest model)
        {
            using (Taller_SysEntities db = new Taller_SysEntities()) {
                var user = db.Usuario.Where(x => x.username == model.Username &&
                x.contrasena == model.Contrasena).FirstOrDefault();

                if (user == null) {
                    return Json(false);
                }
               
                Session["user"] = user;
               
                    
                
                return Json("1");

            }
            
        }

    }
}