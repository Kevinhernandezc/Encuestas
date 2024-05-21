using System.Linq;
using System.Web.Mvc;


namespace Encuestas.Controllers
{
    public class InicioController : Controller
    {
        private encuestasEntities db = new encuestasEntities();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(usuarios model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = db.usuarios.FirstOrDefault(u => u.nombreUsuario == model.nombreUsuario);

            if (user == null || user.password != model.password)
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
                return View(model);
            }

            Session["UserId"] = user.idUsuario;

            return RedirectToAction("Index", "encuestas");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}
