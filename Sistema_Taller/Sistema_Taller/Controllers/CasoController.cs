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
using System.Net;
using Sistema_Taller.Models.Request;

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
                ViewBag.marcas = new SelectList(db.Marca.ToList(), "idMarca", "nombre");
                ViewBag.categoria = new SelectList(db.Categoria.ToList(), "idCategoria", "nombre");
            }
            return View();

        }       

        [HttpPost]
        public ActionResult Crear(CasoViewModel model)
        {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    var dt = new DataTable();
                    dt.Columns.Add("id", typeof(int));
                    dt.Columns.Add("idCaso", typeof(int));
                    dt.Columns.Add("idArticulo", typeof(int));
                    dt.Columns.Add("detalle", typeof(string));
                    dt.Columns.Add("diagnostico", typeof(string));
                    dt.Columns.Add("fechaSalida");
                    dt.Columns.Add("idEstado", typeof(int));

                    var dtArt = new DataTable();
                    dtArt.Columns.Add("idArticulo", typeof(int));
                    dtArt.Columns.Add("nombre", typeof(string));
                    dtArt.Columns.Add("codigo", typeof(string));
                    dtArt.Columns.Add("modelo", typeof(string));
                    dtArt.Columns.Add("idMarca", typeof(int));
                    dtArt.Columns.Add("idCategoria", typeof(int));
                    dtArt.Columns.Add("serie", typeof(string));

                    int i = 0;

                    foreach (var oDetalle in model.Detalles)
                    {
                        dt.Rows.Add(i, i, i, oDetalle.Detalle, null, null, oDetalle.IdEstadoCaso);
                        dtArt.Rows.Add(i, oDetalle.Articulo.Nombre, oDetalle.Articulo.Codigo, oDetalle.Articulo.Modelo,
                            oDetalle.Articulo.IdMarca, oDetalle.Articulo.IdCategoria, oDetalle.Articulo.Serie);
                        i++;
                    }

                    var paramsDet = new SqlParameter("@Detalles", SqlDbType.Structured)
                    {
                        Value = dt,
                        TypeName = "dbo.typ_casodet"
                    };
                    var paramsArt = new SqlParameter("@articulo", SqlDbType.Structured)
                    {
                        Value = dtArt,
                        TypeName = "dbo.typ_articulo"
                    };

                    db.Database.ExecuteSqlCommand("exec Sp_AddCaso @numeroCaso, @idUsuario,@idCliente, @Detalles, @articulo"
                                  , new SqlParameter("@numeroCaso", model.NumeroCaso),
                                  new SqlParameter("@idUsuario", model.IdUsuario),
                                  new SqlParameter("@idCliente", model.IdCliente),
                                  paramsDet, paramsArt);

                }
                return Json("1");
            }catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult Editar(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                if(db.Caso.Find(id) == null)
                {
                    return HttpNotFound();
                }


                ViewBag.estados = new SelectList(db.EstadoCaso.ToList(), "idEstadoCaso", "descripcion");
                ViewBag.marcas = new SelectList(db.Marca.ToList(), "idMarca", "nombre");
                ViewBag.categoria = new SelectList(db.Categoria.ToList(), "idCategoria", "nombre");
            }
            return View();
        }

        [HttpPost]
        public JsonResult Editar(CasoRequest model)
        {
            List<CasoDetalle> detalles = new List<CasoDetalle>();
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {
                    
                    var dt = new DataTable();
                    dt.Columns.Add("id", typeof(int));
                    dt.Columns.Add("idCaso", typeof(int));
                    dt.Columns.Add("idArticulo", typeof(int));
                    dt.Columns.Add("detalle", typeof(string));
                    dt.Columns.Add("diagnostico", typeof(string));
                    dt.Columns.Add("fechaSalida");
                    dt.Columns.Add("idEstado", typeof(int));

                    int i = 0;

                    foreach (var oElement in model.Detalles)
                    {
                        dt.Rows.Add(oElement.IdCasoDetalle,model.IdCaso,oElement.IdArticulo, oElement.Detalle,
                            oElement.Diagnostico, null, oElement.IdEstadoCaso);
                        
                    }

                    var paramsDet = new SqlParameter("@Detalles", SqlDbType.Structured)
                    {
                        Value = dt,
                        TypeName = "dbo.typ_casodet"
                    };

                    db.Database.ExecuteSqlCommand("exec Sp_ActCaso @Detalles", paramsDet);

                }
                return Json("1");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }


        }

        [HttpGet]
        public ActionResult BuscarCaso(int? id)
        {
            CasoViewModel caso = new CasoViewModel();
            using (Taller_SysEntities db = new Taller_SysEntities())
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var oCasoDetalle = db.CasoDetalle.Where(i => i.idCaso == id).ToList();
                var oCaso = db.Caso.Find(id);

                caso.IdCaso = oCaso.idCaso;
                caso.NumeroCaso = oCaso.numero_caso;
                caso.FechaIngreso = oCaso.fecha_ingreso;
                caso.IdUsuario = oCaso.idUsuario;
                caso.IdCliente = oCaso.idCliente;
                caso.Detalles = new List<CasoDetalleViewModel>();

                foreach (var i in oCasoDetalle)
                {
                    CasoDetalleViewModel cd = new CasoDetalleViewModel();
                    cd.IdCasoDetalle = i.idCasoDetalle;
                    cd.Detalle = i.detalle;
                    cd.Diagnostico = i.diagnostico;
                    cd.FechaDespacho = i.fecha_despacho;
                    cd.IdEstadoCaso = i.idEstadoCaso;
                    cd.Articulo = new ArticuloViewModel();
                    var oArticulo = db.Articulo.Where(a => a.idArticulo == i.idArticulo).SingleOrDefault();
                    cd.Articulo.Nombre = oArticulo.nombre;
                    cd.Articulo.Codigo = oArticulo.codigo;
                    cd.Articulo.Modelo = oArticulo.modelo;
                    cd.Articulo.IdMarca = oArticulo.idMarca;
                    cd.Articulo.IdCategoria = oArticulo.idCategoria;
                    cd.Articulo.Serie = oArticulo.serie;
                    cd.Articulo.IdArticulo = oArticulo.idArticulo;
                    caso.Detalles.Add(cd);

                }

            }

            return Json(caso, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Fecha()
        { 
            using (Taller_SysEntities db = new Taller_SysEntities())
            {       
                return Content(db.Database.SqlQuery<DateTime>("Select getdate()").SingleOrDefault().ToString("yyyy-MM-dd"));
            }
            
        }

        [HttpGet]
        public ActionResult NumeroCaso()
        {
            Int64 numCaso = 0;
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                numCaso = 
                    db.Database.SqlQuery<Int64>("SELECT current_value FROM sys.sequences WHERE name = 'CasoNumber'").SingleOrDefault();
            }
            return Content(numCaso.ToString());
        }
    }
}