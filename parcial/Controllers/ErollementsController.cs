using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using parcial.Models;

namespace parcial.Controllers
{
    public class ErollementsController : Controller
    {
        private EscuelaDeConexionespaEntities db = new EscuelaDeConexionespaEntities();

        // GET: Erollements
        public ActionResult Index()
        {
            var erollements = db.Erollements.Include(e => e.Courses).Include(e => e.Students);
            return View(erollements.ToList());
        }

        // GET: Erollements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Erollements erollements = db.Erollements.Find(id);
            if (erollements == null)
            {
                return HttpNotFound();
            }
            return View(erollements);
        }

        // GET: Erollements/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CouserId", "Title");
            ViewBag.StudentID = new SelectList(db.Students, "StudentId", "FirstMidName");
            return View();
        }

        // POST: Erollements/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ErollementID,CourseID,StudentID,Grade")] Erollements erollements)
        {
            if (ModelState.IsValid)
            {
                db.Erollements.Add(erollements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CouserId", "Title", erollements.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentId", "FirstMidName", erollements.StudentID);
            return View(erollements);
        }

        // GET: Erollements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Erollements erollements = db.Erollements.Find(id);
            if (erollements == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CouserId", "Title", erollements.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentId", "FirstMidName", erollements.StudentID);
            return View(erollements);
        }

        // POST: Erollements/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ErollementID,CourseID,StudentID,Grade")] Erollements erollements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(erollements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CouserId", "Title", erollements.CourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentId", "FirstMidName", erollements.StudentID);
            return View(erollements);
        }

        // GET: Erollements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Erollements erollements = db.Erollements.Find(id);
            if (erollements == null)
            {
                return HttpNotFound();
            }
            return View(erollements);
        }

        // POST: Erollements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Erollements erollements = db.Erollements.Find(id);
            db.Erollements.Remove(erollements);
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
