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
using System.Collections;
using Microsoft.AspNet.Identity;

namespace Safety.Controllers
{
    public class TestingController : Controller
    {
        SafetyContext db = new SafetyContext();

        // GET: Manager/Specialities
        public ActionResult Index()
        {
            //Вывод всех специальностей для выбора пользователем
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
        [HttpGet]
        public ActionResult Ticket(int id)
        {
            var ticket = db.Tickets
                .Include(x => x.Speciality)
                .Where(x => x.Speciality.Id == id);
            
            if (ticket.Count() > 0)
            {             
                int[] allId = ticket.Select(x => x.Id).ToArray();   //Массив всех id билета по теме для выбора случайного

                //Генерация случайного id
                Random rand = new Random();
                int randId;
                randId = rand.Next(0, allId.Length);
                id = allId[randId];

                var selectTicket = db.Tickets
                                    .Include(x => x.Speciality)
                                    .Where(x => x.Id == id)
                                    .First();
                Session["ticket"] = id;
                Session["questionIndex"] = 0;
                Session["score"] = 0;

                return View(selectTicket);
            }

        return View("NoFoundTicket");
        }

        public ActionResult TestProcess()
        {
            int index = Convert.ToInt32(Session["questionIndex"]);
            int ticketId = Convert.ToInt32(Session["ticket"]);

            var questions = db.QuestAndAnsws                        //Выборка вопросов по билету
                            .Where(x => x.TicketId == ticketId)
                            .ToList(); 

            int count = questions.Count();                          //Счётчик всех вопросов для отображения и условия окончания
            int numQuestion = index+1;                              //Для отображени начиная с единицы
            
            if (numQuestion > questions.Count())                    //Последний вопрос перенаправляет на отчётность
            {
                return RedirectToRoute(new { controller = "Testing", action = "Result" });
            }

            ViewBag.numQuestion = numQuestion;
            ViewBag.ticketId = ticketId;
            ViewBag.count = count;

            if (index == 0)
            {
                return View(questions.ElementAtOrDefault(0));
            }

            var question = questions.ElementAtOrDefault(index);
            return View(question);
        }

        [HttpPost]
        public ActionResult Question(int id)
        {
            int index = Convert.ToInt32(Session["questionIndex"]);
            int ticketId = Convert.ToInt32(Session["ticket"]);

            var answers = db.QuestAndAnsws                            //Выборка вопросов по билету
                            .Where(x => x.TicketId == ticketId)
                            .Select(x => x.AnswerTrue)
                            .ToList();

            //Проверка ответа
            QuestAndAnsw qanawer = new QuestAndAnsw();
            int tryAnswer = answers.ElementAtOrDefault(index);
            if (id == tryAnswer)
            {
                int score = Convert.ToInt32(Session["score"]);
                Session["score"] = score+1;
            }

            Session["questionIndex"] = index+1;                     //Переход к след вопросу в билете

            return RedirectToRoute(new { controller = "Testing", action = "TestProcess" });
        }

        public ActionResult Result()
        {
            int ticketId = Convert.ToInt32(Session["ticket"]);
            var count = db.QuestAndAnsws                        //Подсчёт кол-ва вопросов в билете
                 .Where(x => x.TicketId == ticketId)
                 .Count();

            ViewBag.score = Convert.ToInt32(Session["Score"]);
            ViewBag.count = count;

            Result result = new Result();
            result.Date = DateTime.Now.Date;
            result.UserId = User.Identity.GetUserId();
            result.Score = Convert.ToInt32(Session["Score"]);
            result.SpecialityId = db.Tickets
                                    .Where(x => x.Id == ticketId)
                                    .Select(x => x.SpecialityId).FirstOrDefault() ?? 0;

            //Session["ticket"] = null;
            //Session["questionIndex"] = null;
            //Session["Score"] = null;

            db.Results.Add(result);
            db.SaveChanges();

            return View();
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
