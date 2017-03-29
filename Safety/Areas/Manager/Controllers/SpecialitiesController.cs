using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Safety;
using Safety.Models;

namespace Safety.Areas.Manager.Controllers
{
    public class SpecialitiesController : Controller
    {
         SafetyContext db = new SafetyContext();

        // GET: Manager/Specialities
        public ActionResult Index()
        {
            SelectList typeTests = new SelectList(db.TypeTests, "Id", "Name");
            ViewBag.TypeTests = typeTests;

            ViewBag.TypeTest = db.TypeTests.ToList();
            var specialitys = db.Specialitys.Include(s => s.TypeTest);
            return View(specialitys.ToList());
        }

        //GET: Ajax filer
        [HttpPost]
        public ActionResult SpecialitiesSearch(string typeTestsId)
        {
            int id = Convert.ToInt32(typeTestsId);

            if (id == 0)
            {
                var specialities = db.Specialitys.Include(s => s.TypeTest).ToList();
                return PartialView(specialities);
            }
            else
            {
                var specialities = db.Specialitys.Include(s => s.TypeTest).Where(a => a.TypeTestId == id).ToList();
                if (specialities.Count <= 0)
                {
                    return HttpNotFound();
                }
                return PartialView(specialities);
            }
        }

        // GET: Manager/Specialities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = db.Specialitys.Include(s => s.TypeTest).FirstOrDefault(x => x.Id == id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            return View(speciality);
        }

        // GET: Manager/Specialities/Create
        public ActionResult Create()
        {
            ViewBag.TypeTestId = new SelectList(db.TypeTests, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,TypeTestId")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                db.Specialitys.Add(speciality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeTestId = new SelectList(db.TypeTests, "Id", "Name", speciality.TypeTestId);
            return View(speciality);
        }

        // GET: Manager/Specialities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = db.Specialitys.Find(id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeTestId = new SelectList(db.TypeTests, "Id", "Name", speciality.TypeTestId);
            return View(speciality);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,TypeTestId")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(speciality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeTestId = new SelectList(db.TypeTests, "Id", "Name", speciality.TypeTestId);
            return View(speciality);
        }

        // GET: Manager/Specialities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = db.Specialitys.Include(s => s.TypeTest).FirstOrDefault(x => x.Id == id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            return View(speciality);
        }

        // POST: Manager/Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Speciality speciality = db.Specialitys.Find(id);
            db.Specialitys.Remove(speciality);
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
