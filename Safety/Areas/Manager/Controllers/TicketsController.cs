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
    public class TicketsController : Controller
    {
        private SafetyContext db = new SafetyContext();

        // GET: Manager/Tickets
        public ActionResult Index()
        {
            int selectedIndex = 0;
            SelectList typeTests = new SelectList(db.TypeTests.OrderBy(x => x.Name), "Id", "Name", selectedIndex);
            ViewBag.TypeTests = typeTests;

            SelectList specialitys = new SelectList(db.Specialitys.OrderBy(x => x.Name).OrderBy(x => x.Name), "Id", "Name", selectedIndex);
            ViewBag.Specialitys = specialitys;

            var tickets = db.Tickets.Include(t => t.Speciality);
            return View(tickets.ToList());
        }
        public ActionResult GetItemsPartial(int id)
        {
            if (id == 0)
            {
                return PartialView(db.Specialitys.ToList());
            }
            return PartialView(db.Specialitys.Where(c => c.TypeTestId == id).ToList());
        }

        //GET: Ajax filer
        [HttpPost]
        public ActionResult TicketsSearch(int typeTestsId, int specialitysId)
        {
            if(typeTestsId == 0)
            {
                var result = db.Tickets.Include(t => t.Speciality).ToList();
                return PartialView(result);
            }
            if(typeTestsId != 0 && specialitysId != 0)
            {
                var result = db.Tickets
                    .Include(x => x.Speciality.TypeTest)
                    .Where(x => x.Speciality.TypeTest.Id == typeTestsId)
                    .Where(x => x.Speciality.Id == specialitysId);
                return PartialView(result);
            }
            if (typeTestsId != 0 && specialitysId == 0)
            {
                var result = db.Tickets
                    .Include(x => x.Speciality.TypeTest)
                    .Where(x => x.Speciality.TypeTest.Id == typeTestsId);
                return PartialView(result);
            }
            else
                return PartialView(db.Tickets.Include(t => t.Speciality).ToList());
        }

        // GET: Manager/Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Include(x => x.Speciality).FirstOrDefault(x => x.Id == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Manager/Tickets/Create
        public ActionResult Create()
        {
            ViewBag.SpecialityId = new SelectList(db.Specialitys, "Id", "Name");
            return View();
        }

        // POST: Manager/Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nomber,SpecialityId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialityId = new SelectList(db.Specialitys, "Id", "Name", ticket.SpecialityId);
            return View(ticket);
        }

        // GET: Manager/Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecialityId = new SelectList(db.Specialitys, "Id", "Name", ticket.SpecialityId);
            return View(ticket);
        }

        // POST: Manager/Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nomber,SpecialityId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialityId = new SelectList(db.Specialitys, "Id", "Name", ticket.SpecialityId);
            return View(ticket);
        }

        // GET: Manager/Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Include(x => x.Speciality).FirstOrDefault(x => x.Id == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Manager/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
