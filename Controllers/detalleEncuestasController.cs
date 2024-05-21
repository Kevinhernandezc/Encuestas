using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Encuestas;

namespace Encuestas.Controllers
{
    public class detalleEncuestasController : Controller
    {
        private encuestasEntities db = new encuestasEntities();

        // GET: detalleEncuestas
        public ActionResult Index(int id)
        {
            if (id != 0)
            {
                Session["idEncuesta"] = id;
            }
            else {
                id = (int)Session["idEncuesta"];
            }
            var detalleEncuestas = db.detalleEncuestas
                                     .Include(d => d.encuestas)
                                     .Where(d => d.idEncuesta == id)
                                     .ToList();


            return View(detalleEncuestas);
        }

        // GET: detalleEncuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleEncuestas detalleEncuestas = db.detalleEncuestas.Find(id);
            if (detalleEncuestas == null)
            {
                return HttpNotFound();
            }
            return View(detalleEncuestas);
        }

        // GET: detalleEncuestas/Create
        public ActionResult Create()
        {
            ViewBag.idEncuesta = new SelectList(db.encuestas, "idEncuesta", "nombreEncuesta");
            return View();
        }

        // POST: detalleEncuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalleEncuesta,nombreCampo,titulo,esRequerido,tipoCampo")] detalleEncuestas detalleEncuestas)
        {
            if (ModelState.IsValid)
            {
                if (Session["idEncuesta"] != null)
                {
                    detalleEncuestas.idEncuesta = (int)Session["idEncuesta"];
                    db.detalleEncuestas.Add(detalleEncuestas);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = 0 });
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo determinar la encuesta actual.");
                }
            }

            ViewBag.idEncuesta = new SelectList(db.encuestas, "idEncuesta", "nombreEncuesta", detalleEncuestas.idEncuesta);
            return View(detalleEncuestas);
        }

        // GET: detalleEncuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleEncuestas detalleEncuestas = db.detalleEncuestas.Find(id);
            if (detalleEncuestas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEncuesta = new SelectList(db.encuestas, "idEncuesta", "nombreEncuesta", detalleEncuestas.idEncuesta);
            return View(detalleEncuestas);
        }

        // POST: detalleEncuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetalleEncuesta,nombreCampo,titulo,esRequerido,tipoCampo")] detalleEncuestas detalleEncuestas)
        {
            if (ModelState.IsValid)
            {
                detalleEncuestas.idEncuesta = (int)Session["idEncuesta"];
                db.Entry(detalleEncuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = 0 });
            }
            ViewBag.idEncuesta = new SelectList(db.encuestas, "idEncuesta", "nombreEncuesta", detalleEncuestas.idEncuesta);
            return View(detalleEncuestas);
        }

        // GET: detalleEncuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detalleEncuestas detalleEncuestas = db.detalleEncuestas.Find(id);
            if (detalleEncuestas == null)
            {
                return HttpNotFound();
            }
            return View(detalleEncuestas);
        }

        // POST: detalleEncuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detalleEncuestas detalleEncuestas = db.detalleEncuestas.Find(id);
            db.detalleEncuestas.Remove(detalleEncuestas);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = 0 });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult encuestaGenerada(int id) {
            Session["idEncuesta"] = id;
            var detalleEncuestas = db.detalleEncuestas
                                     .Include(d => d.encuestas)
                                     .Where(d => d.idEncuesta == id)
                                     .ToList();

            return View(detalleEncuestas);
        }

        [HttpPost]
        public ActionResult encuestaGenerada(List<respuestasEncuestas> respuestas)
        {
            if (ModelState.IsValid)
            {
                int idEncuesta = (int)Session["idEncuesta"]; // Asegúrate de que esto esté correctamente establecido.

                foreach (var respuesta in respuestas)
                {
                    var nuevaRespuesta = new respuestasEncuestas
                    {
                        idPregunta = respuesta.idPregunta,
                        respuesta = respuesta.respuesta,
                        fecha = DateTime.Today
                    };

                    db.respuestasEncuestas.Add(nuevaRespuesta);
                }
                db.SaveChanges();
                return RedirectToAction("Index","encuestas");
            }

            // Si algo falla, vuelve a cargar el formulario.
            return View(respuestas);
        }
    }
}
