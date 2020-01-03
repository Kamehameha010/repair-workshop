using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data.SqlClient;
using Sistema_Taller.Models;


namespace Sistema_Taller.Models
{
    public static class ListaComboBox
    {
        public static System.Web.Mvc.SelectList estados()
        { 
            System.Web.Mvc.SelectList p = null;
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
               return  p = new System.Web.Mvc.SelectList(db.Estado.ToList(), "IdEstado", "descripcion");
            }         
        }

        public static System.Web.Mvc.SelectList roles()
        {
            System.Web.Mvc.SelectList p = null;
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                return p = new System.Web.Mvc.SelectList(db.Rol.ToList(), "idRol", "nombre");
            }
        }
    }
}