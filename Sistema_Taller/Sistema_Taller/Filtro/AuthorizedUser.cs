using Sistema_Taller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Filtro
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizedUser:AuthorizeAttribute
    {
        private Usuario oUsuario;
        private int idOperacion;

        public AuthorizedUser(int idOperacion = 0) {
            this.idOperacion = idOperacion;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                oUsuario = HttpContext.Current.Session["user"] as Usuario;
                int count = 0;
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var lstOperacion = db.Rol_Operacion
                        .Where(x => x.idRol == oUsuario.idRol && x.idOperacion == idOperacion);
                    count = lstOperacion.Count();
                }

                if(count == 0)
                {
                    filterContext.Result = new RedirectResult("~/Access/Login");
                }

            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("~/Access/Login");
            }
            
        }
    }
}