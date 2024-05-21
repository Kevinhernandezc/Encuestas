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
    public class encuestasController : Controller
    {
        private encuestasEntities db = new encuestasEntities();

        // GET: encuestas
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "inicio");
            }
            return View(db.encuestas.ToList());
        }

        // GET: encuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuestas encuestas = db.encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // GET: encuestas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: encuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEncuesta,nombreEncuesta,descripcion")] encuestas encuestas)
        {
            if (ModelState.IsValid)
            {
                encuestas.fecha = DateTime.Today;
                db.encuestas.Add(encuestas);
                db.SaveChanges();
                return RedirectToAction("Index", "DetalleEncuestas", new { id = encuestas.idEncuesta });
            }

            return View(encuestas);
        }

        // GET: encuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuestas encuestas = db.encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // POST: encuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEncuesta,nombreEncuesta,descripcion")] encuestas encuestas)
        {
            if (ModelState.IsValid)
            {
                encuestas.fecha = DateTime.Today;
                db.Entry(encuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(encuestas);
        }

        // GET: encuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuestas encuestas = db.encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // POST: encuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            encuestas encuestas = db.encuestas.Find(id);
            db.encuestas.Remove(encuestas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
