using Safety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Safety.Areas.Manager.Controllers
{
    public class HomeController : Controller
    {
        SafetyContext db = new SafetyContext();
        // GET: Manager/Home
        public ActionResult Index()
        {
            var typeTests = db.TypeTests.Count();
            ViewBag.CountTypeTests = typeTests;
            var specialities = db.Specialitys.Count();
            ViewBag.Specialities = specialities;
            var tickets = db.Tickets.Count();
            ViewBag.Tickets = tickets;
            var questAndAnsws = db.QuestAndAnsws.Count();
            ViewBag.QuestAndAnsws = questAndAnsws;

            return View();
        }
    }
}