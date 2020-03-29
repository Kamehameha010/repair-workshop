using Sistema_Taller.Controllers;
using Sistema_Taller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Taller.Filtro
{
    public class ValidatedSession: ActionFilterAttribute
    {

        private Usuario oUsuario = null;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                oUsuario = HttpContext.Current.Session["user"] as Usuario;

                if( oUsuario == null)
                {
                    if(filterContext.Controller is AccessController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Access/Login");
                    }
                }
            }
            catch (Exception e)
            {

                filterContext.Result = new RedirectResult("~/Access/Login");
            } 
            

        }
    }
}