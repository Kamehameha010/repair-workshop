using System.Web.Mvc;

namespace Sistema_Taller.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult add()
        {
            try
            {

            }
            catch
            {

            }

            return RedirectToAction("Index");
        }
    }
}