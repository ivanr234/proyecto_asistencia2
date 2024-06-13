using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary11;

namespace Proyecto_final.Controllers
{
    public class InstructoresController : Controller
    {
        private GestionAcademicaEntities db = new GestionAcademicaEntities();

        // GET: Instructores
        public ActionResult Index()
        {
            var instructores = db.Instructores.Include(i => i.Fichas);
            return View(instructores.ToList());
        }

        // GET: Instructores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructores instructores = db.Instructores.Find(id);
            if (instructores == null)
            {
                return HttpNotFound();
            }
            return View(instructores);
        }

        // GET: Instructores/Create
        public ActionResult Create()
        {
            ViewBag.ficha_id = new SelectList(db.Fichas, "ficha_id", "numero_ficha");
            return View();
        }

        // POST: Instructores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "instructor_id,nombre,apellido,imagen,correo_electronico,contraseña,numero_documento,tipo_documento,direccion,numero,tipo_instructor,ficha_id")] Instructores instructores)
        {
            if (ModelState.IsValid)
            {
                db.Instructores.Add(instructores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ficha_id = new SelectList(db.Fichas, "ficha_id", "numero_ficha", instructores.ficha_id);
            return View(instructores);
        }

        // GET: Instructores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructores instructores = db.Instructores.Find(id);
            if (instructores == null)
            {
                return HttpNotFound();
            }
            ViewBag.ficha_id = new SelectList(db.Fichas, "ficha_id", "numero_ficha", instructores.ficha_id);
            return View(instructores);
        }

        // POST: Instructores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "instructor_id,nombre,apellido,imagen,correo_electronico,contraseña,numero_documento,tipo_documento,direccion,numero,tipo_instructor,ficha_id")] Instructores instructores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ficha_id = new SelectList(db.Fichas, "ficha_id", "numero_ficha", instructores.ficha_id);
            return View(instructores);
        }

        // GET: Instructores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructores instructores = db.Instructores.Find(id);
            if (instructores == null)
            {
                return HttpNotFound();
            }
            return View(instructores);
        }

        // POST: Instructores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructores instructores = db.Instructores.Find(id);
            db.Instructores.Remove(instructores);
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
