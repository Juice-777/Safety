using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Safety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace Safety.Areas.Manager.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        SafetyContext db = new SafetyContext();

        // GET: Manager/Users
        public ActionResult Index()
        {
            var allusers = context.Users.ToList();
            return View(allusers);
        }
        //AJAX
        public ActionResult SearchName(string SearchName)
        {
            var someusers = context.Users
                            .Where(x => x.LName == SearchName)
                            .ToList();

            return PartialView("NamesPartial", someusers);
        }
        public ActionResult Results(string id)
        {
            var results = db.Results
                        .Include(i => i.Speciality)
                        .Where(x => x.UserId == id)
                        .ToList();

            return View(results);
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