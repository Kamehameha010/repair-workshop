﻿using Sistema_Taller.Models;
using Sistema_Taller.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class AccessController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        public JsonResult Login(LoginRequest model)
        
        {
            using (Taller_SysEntities db = new Taller_SysEntities()) {
                var user = db.Usuario.Where(x => x.username == model.Username &&
                x.contrasena == model.Contrasena && x.idEstado == 1).FirstOrDefault();

                if (user == null) {
                    return Json("0");
                }
                Session["user"] = user;
                return Json("1");
            }
            
        }

    }
}