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
    public class StudentsController : Controller
    {
        private EscuelaDeConexionespaEntities db = new EscuelaDeConexionespaEntities();
        
        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
        public ActionResult Cursos()
        {
            var informationofStudents = db.Students.ToList();
            var informationofCourse = db.Courses.ToList();


            List<SelectListItem> ComboboxOfStudents = new List<SelectListItem>();
            List<SelectListItem> ComboboxOfCourses = new List<SelectListItem>();

            foreach (var iteracion in informationofStudents)
            {


                ComboboxOfStudents.Add(new SelectListItem


                {
                    Text = iteracion.FirstMidName,
                    Value = Convert.ToString(iteracion.StudentId)


                }




                    );


                ViewBag.listofstudentcombobox = ComboboxOfStudents;


            }




            foreach (var iteracion in informationofCourse)
            {


                ComboboxOfCourses.Add(new SelectListItem


                {
                    Text = iteracion.Title,
                    Value = Convert.ToString(iteracion.CouserId)


                }




                    );


                ViewBag.listofcoursescombobox = ComboboxOfCourses;


            }


            return View();
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // GET: Students/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,LastName,FirstMidName,EnrrollmentsDate")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(students);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(students);
        }

        // GET: Students/Edit/5
        bool edit = false;
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return View();
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            edit = true;
            return View(students);
        }

        // POST: Students/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "StudentId,LastName,FirstMidName,EnrrollmentsDate")] Students students)
        {
            if (edit)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(students).State = EntityState.Modified;
                    db.SaveChanges();
                    edit = false;
                    return RedirectToAction("Index");
                }
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(students);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(students);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Students students = db.Students.Find(id);
            db.Students.Remove(students);
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
