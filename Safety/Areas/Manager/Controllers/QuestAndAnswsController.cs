using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Safety.Models;

namespace Safety.Areas.Manager.Controllers
{
    public class QuestAndAnswsController : Controller
    {
        private SafetyContext db = new SafetyContext();

        // GET: Manager/QuestAndAnsws
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Id = id;
            var questAndAnsws = db.QuestAndAnsws.Include(q => q.Ticket).Where(x => x.TicketId == id);
            return View(questAndAnsws.ToList());
        }

        // GET: Manager/QuestAndAnsws/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestAndAnsw questAndAnsw = db.QuestAndAnsws.Find(id);
            if (questAndAnsw == null)
            {
                return HttpNotFound();
            }
            return View(questAndAnsw);
        }

        // GET: Manager/QuestAndAnsws/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Id = id;
            //ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Id");
            return View();
        }

        // POST: Manager/QuestAndAnsws/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Question,Answer1,Answer2,Answer3,Answer4,AnswerTrue,TicketId")] QuestAndAnsw questAndAnsw)
        {
            if (ModelState.IsValid)
            {
                db.QuestAndAnsws.Add(questAndAnsw);
                db.SaveChanges();
                return RedirectToAction("Index", "QuestAndAnsws", new { id = questAndAnsw.TicketId});
            }

            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Id", questAndAnsw.TicketId);
            return View(questAndAnsw);
        }

        // GET: Manager/QuestAndAnsws/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestAndAnsw questAndAnsw = db.QuestAndAnsws.Find(id);
            if (questAndAnsw == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Id", questAndAnsw.TicketId);
            return View(questAndAnsw);
        }

        // POST: Manager/QuestAndAnsws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Question,Answer1,Answer2,Answer3,Answer4,AnswerTrue,TicketId")] QuestAndAnsw questAndAnsw)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questAndAnsw).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "QuestAndAnsws", new { id = questAndAnsw.TicketId });
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Id", questAndAnsw.TicketId);
            return View(questAndAnsw);
        }

        // GET: Manager/QuestAndAnsws/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestAndAnsw questAndAnsw = db.QuestAndAnsws.Find(id);
            if (questAndAnsw == null)
            {
                return HttpNotFound();
            }
            return View(questAndAnsw);
        }

        // POST: Manager/QuestAndAnsws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {

            QuestAndAnsw questAndAnsw = db.QuestAndAnsws.Find(id);
            //Для параметра редиректа Id к вопросам билета
            int ticketId = questAndAnsw.TicketId ?? 0;
            db.QuestAndAnsws.Remove(questAndAnsw);
            db.SaveChanges();

            return RedirectToAction("Index", "QuestAndAnsws", new { id = ticketId });
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
