using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Encuestas.Controllers
{
    public class RespuestasController : Controller
    {
        private encuestasEntities db = new encuestasEntities();
        // GET: Respuestas

        public ActionResult respuestas(int id)
        {
            var respuestasEncuestas = db.respuestasEncuestas
                                    .Include("detalleEncuestas")
                                    .Include("detalleEncuestas.encuestas")
                                    .Where(r => r.detalleEncuestas.idEncuesta == id)
                                    .ToList();

            if (respuestasEncuestas.Any())
            {
                ViewBag.NombreEncuesta = respuestasEncuestas.FirstOrDefault().detalleEncuestas.encuestas.nombreEncuesta;
            }
            else
            {
                ViewBag.NombreEncuesta = "Nombre no disponible";
            }

            return View(respuestasEncuestas);
        }
    }
}