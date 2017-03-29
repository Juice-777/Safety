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
    public class TypeTestsController : Controller
    {
        private SafetyContext db = new SafetyContext();

        // GET: Manager/TypeTests
        public ActionResult Index()
        {
            return View(db.TypeTests.ToList());
        }

        // GET: Manager/TypeTests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeTest typeTest = db.TypeTests.Find(id);
            if (typeTest == null)
            {
                return HttpNotFound();
            }
            return View(typeTest);
        }

        // GET: Manager/TypeTests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/TypeTests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TypeTest typeTest)
        {
            if (ModelState.IsValid)
            {
                db.TypeTests.Add(typeTest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeTest);
        }

        // GET: Manager/TypeTests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeTest typeTest = db.TypeTests.Find(id);
            if (typeTest == null)
            {
                return HttpNotFound();
            }
            return View(typeTest);
        }

        // POST: Manager/TypeTests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TypeTest typeTest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeTest);
        }

        // GET: Manager/TypeTests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeTest typeTest = db.TypeTests.Find(id);
            if (typeTest == null)
            {
                return HttpNotFound();
            }
            return View(typeTest);
        }

        // POST: Manager/TypeTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeTest typeTest = db.TypeTests.Find(id);
            db.TypeTests.Remove(typeTest);
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
