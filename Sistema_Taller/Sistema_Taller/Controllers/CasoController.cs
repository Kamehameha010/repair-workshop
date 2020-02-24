using Sistema_Taller.Models;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Sistema_Taller.Controllers
{
    public class CasoController : Controller
    {

        private string draw = "";
        private string start = "";
        private string length = "";
        private string sortColumn = "";
        private string sortColumnDir = "";
        private string searchValue = "";
        private int pageSize, skip, recordsTotal;
        // GET: Caso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Casos()
        {
            List<View_Caso> lst = new List<View_Caso>();
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
                IQueryable<View_Caso> query = db.View_Caso;

                if (searchValue.Length > 0)
                {
                    query = query.Where(x => x.numero_caso.ToString().Contains(searchValue) || x.Articulo.Contains(searchValue) || x.Cliente.Contains(searchValue));
                }
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsTotal = query.Count();

                lst = query.Skip(skip).Take(pageSize).ToList();
            }
            return Json(new {  draw, recordsFiltered = recordsTotal, recordsTotal, data = lst });
        }


        [HttpGet]
        public ActionResult Crear()
        {

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                ViewBag.estados = new SelectList(db.EstadoCaso.ToList(), "idEstadoCaso", "descripcion");
            }
            return View();

        }       

        [HttpPost]
        public ActionResult Crear(CasoViewModel model)
        {

            return View(model);
        }

        [HttpGet]
        public ActionResult Fecha()
        { 
            using (Taller_SysEntities db = new Taller_SysEntities())
            {       
                return Content(db.Database.SqlQuery<DateTime>("Select getdate()").SingleOrDefault().ToString("yyyy-MM-dd"));
            }
            
        }
    }
}