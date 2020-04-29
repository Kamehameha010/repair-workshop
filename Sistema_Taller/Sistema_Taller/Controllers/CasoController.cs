using Microsoft.Ajax.Utilities;
using Sistema_Taller.Models;
using Sistema_Taller.Models.Request;
using Sistema_Taller.Models.Response;
using Sistema_Taller.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Mvc;

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
                    query = query.Where(x => x.Numerocaso.ToString().Contains(searchValue) || x.Usuario.Contains(searchValue) || x.Cliente.Contains(searchValue));
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
                using(Taller_SysEntities db = new Taller_SysEntities())
                {
                    ViewBag.estados = new SelectList(db.EstadoCaso.ToList(), "idEstadoCaso", "descripcion");
                    ViewBag.marcas = new SelectList(db.Marca.ToList(), "idMarca", "nombre");
                    ViewBag.categoria = new SelectList(db.Categoria.ToList(), "idCategoria", "nombre");
                }

                if (ModelState.IsValid)
                {
                    using (Taller_SysEntities db = new Taller_SysEntities())
                    {
                        var dt = new DataTable();
                        dt.Columns.Add("id", typeof(int));
                        dt.Columns.Add("idCaso", typeof(int));
                        dt.Columns.Add("idArticulo", typeof(int));
                        dt.Columns.Add("detalle", typeof(string));
                        dt.Columns.Add("diagnostico", typeof(string));


                        var dtArt = new DataTable();
                        dtArt.Columns.Add("idArticulo", typeof(int));
                        dtArt.Columns.Add("nombre", typeof(string));
                        dtArt.Columns.Add("codigo", typeof(string));
                        dtArt.Columns.Add("modelo", typeof(string));
                        dtArt.Columns.Add("idMarca", typeof(int));
                        dtArt.Columns.Add("idCategoria", typeof(int));
                        dtArt.Columns.Add("serie", typeof(string));

                        int i = 0;

                        foreach (var oDetalle in model.CasoDetalle)
                        {
                            dt.Rows.Add(i, i, i, oDetalle.Detalle, null);
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

                        db.Database.ExecuteSqlCommand("exec Sp_AddCaso @numeroCaso, @idUsuario,@idCliente,@idEstado,  @Detalles, @articulo"
                                      , new SqlParameter("@numeroCaso", model.NumeroCaso),
                                      new SqlParameter("@idUsuario", model.IdUsuario),
                                      new SqlParameter("@idCliente", model.IdCliente),
                                      new SqlParameter("@idEstado", model.IdEstadoCaso),
                                      paramsDet, paramsArt);

                    }
                    return Json("1");
                }
                return View(model);
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
                    
                    foreach (var oElement in model.CasoDetalle)
                    {
                        dt.Rows.Add(oElement.IdCasoDetalle,model.IdCaso,oElement.IdArticulo, oElement.Detalle,
                            oElement.Diagnostico);
                       
                    }

                    var paramsDet = new SqlParameter("@Detalles", SqlDbType.Structured)
                    {
                        Value = dt,
                        TypeName = "dbo.typ_casodet"
                    };

                    db.Database.ExecuteSqlCommand("exec Sp_ActCaso @IdCaso, @IdEstadoCaso, @Detalles",
                        new SqlParameter("@IdCaso", model.IdCaso),
                        new SqlParameter("@IdEstadoCaso", model.IdEstadoCaso),
                        paramsDet);
                    
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            using (Taller_SysEntities db = new Taller_SysEntities())
            {
                if (db.fnCaso(id).ToList().Count == 0)
                    return HttpNotFound();

                var oCaso = db.fnCaso(id)
                    .Select(x => new CasoResponse
                    {
                        IdCaso = x.idCaso,
                        FechaIngreso = x.fecha_ingreso,
                        NumeroCaso = x.numero_caso,
                        IdUsuario = x.idUsuario,
                        Username = x.username,
                        IdCliente = x.idCliente,
                        Cliente = x.nombre,
                        IdEstadoCaso = x.idEstadoCaso,
                        FechaDespacho = x.fecha_despacho
                    }).FirstOrDefault();

                oCaso.CasoDetalle = db.fnCasoDetalle(id)
                    .Select(x => new CasoDetalleResponse
                    {
                        IdCasoDetalle = x.idCasoDetalle,
                        IdCaso = oCaso.IdCaso,
                        IdArticulo = x.idArticulo,
                        Articulo = x.nombre,
                        Codigo = x.codigo,
                        Marca = x.idMarca,
                        Categoria = x.idCategoria,
                        Modelo = x.modelo,
                        Serie = x.serie,
                        Observacion = x.detalle,
                        Diagnostico = x.diagnostico
                    }).ToList();

                return Json(oCaso, JsonRequestBehavior.AllowGet);
            }
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
        [HttpPost]
        public JsonResult Cerrar(int id)
        {
            try
            {
                using (Taller_SysEntities db = new Taller_SysEntities())
                {

                    var oCaso = db.Caso.Find(id);
                    oCaso.idEstadoCaso = 3;
                    db.Entry(oCaso).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Json("1");

                }
            }
            catch (Exception)
            {
                return Json("0");
            }
        }
    }
}