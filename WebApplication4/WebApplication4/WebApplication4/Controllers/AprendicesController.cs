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
    public class AprendicesController : Controller
    {
        private GestionAcademicaEntities db = new GestionAcademicaEntities();

        // GET: Aprendices
        public ActionResult Index()
        {
            return View(db.Aprendices.ToList());
        }

        // GET: Aprendices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendices aprendices = db.Aprendices.Find(id);
            if (aprendices == null)
            {
                return HttpNotFound();
            }
            return View(aprendices);
        }

        // GET: Aprendices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aprendices/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aprendiz_id,nombre,apellido,correo_electronico,contraseña,numero_documento,tipo_documento,numero_aprendiz,direccion,correo,estado")] Aprendices aprendices)
        {
            if (ModelState.IsValid)
            {
                db.Aprendices.Add(aprendices);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aprendices);
        }

        // GET: Aprendices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendices aprendices = db.Aprendices.Find(id);
            if (aprendices == null)
            {
                return HttpNotFound();
            }
            return View(aprendices);
        }

        // POST: Aprendices/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aprendiz_id,nombre,apellido,correo_electronico,contraseña,numero_documento,tipo_documento,numero_aprendiz,direccion,correo,estado")] Aprendices aprendices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aprendices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aprendices);
        }

        // GET: Aprendices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendices aprendices = db.Aprendices.Find(id);
            if (aprendices == null)
            {
                return HttpNotFound();
            }
            return View(aprendices);
        }

        // POST: Aprendices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aprendices aprendices = db.Aprendices.Find(id);
            db.Aprendices.Remove(aprendices);
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
