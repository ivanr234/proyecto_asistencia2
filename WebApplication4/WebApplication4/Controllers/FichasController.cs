using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassLibrary1;

namespace WebApplication4.Controllers
{
    public class FichasController : Controller
    {
        private GestionAcademicaEntities1 db = new GestionAcademicaEntities1();

        // GET: Fichas
        public ActionResult Index()
        {
            var fichas = db.Fichas.Include(f => f.Instructores).Include(f => f.Programas);
            return View(fichas.ToList());
        }

        // GET: Fichas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fichas fichas = db.Fichas.Find(id);
            if (fichas == null)
            {
                return HttpNotFound();
            }
            return View(fichas);
        }

        // GET: Fichas/Create
        public ActionResult Create()
        {
            ViewBag.instructor_id = new SelectList(db.Instructores, "instructor_id", "nombre");
            ViewBag.programa_id = new SelectList(db.Programas, "programa_id", "nombre_programa");
            return View();
        }

        // POST: Fichas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ficha_id,numero_ficha,cupo_ficha,tipo_ficha,fecha_inicio,fecha_fin,programa_id,instructor_id")] Fichas fichas)
        {
            if (ModelState.IsValid)
            {
                db.Fichas.Add(fichas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.instructor_id = new SelectList(db.Instructores, "instructor_id", "nombre", fichas.instructor_id);
            ViewBag.programa_id = new SelectList(db.Programas, "programa_id", "nombre_programa", fichas.programa_id);
            return View(fichas);
        }

        // GET: Fichas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fichas fichas = db.Fichas.Find(id);
            if (fichas == null)
            {
                return HttpNotFound();
            }
            ViewBag.instructor_id = new SelectList(db.Instructores, "instructor_id", "nombre", fichas.instructor_id);
            ViewBag.programa_id = new SelectList(db.Programas, "programa_id", "nombre_programa", fichas.programa_id);
            return View(fichas);
        }

        // POST: Fichas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ficha_id,numero_ficha,cupo_ficha,tipo_ficha,fecha_inicio,fecha_fin,programa_id,instructor_id")] Fichas fichas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fichas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.instructor_id = new SelectList(db.Instructores, "instructor_id", "nombre", fichas.instructor_id);
            ViewBag.programa_id = new SelectList(db.Programas, "programa_id", "nombre_programa", fichas.programa_id);
            return View(fichas);
        }

        // GET: Fichas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fichas fichas = db.Fichas.Find(id);
            if (fichas == null)
            {
                return HttpNotFound();
            }
            return View(fichas);
        }

        // POST: Fichas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fichas fichas = db.Fichas.Find(id);
            db.Fichas.Remove(fichas);
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
