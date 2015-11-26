using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity.Models;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Bisness_Logic;

namespace Entity.Controllers
{
    public class PeopleController : Controller
    {
        private EntityContext db = new EntityContext();

        //
        // GET: /People/
        public PeopleController() { }
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }
        public ActionResult PopularVideo()
        {
            DateTime day = DateTime.Now;
            Video popularVideo = db.PopularVideo.FirstOrDefault(x =>
                    x.DateTime.Day == day.Day
                    && x.DateTime.Month == day.Month
                    && x.DateTime.Year == day.Year);
            ViewBag.PopularVideoPlayer = popularVideo.Player;
            return View();
        }
        //
        // GET: /People/Details/5

        public ActionResult Details(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // GET: /People/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /People/Create

        [HttpPost]
        public ActionResult Create(string userUrl, bool addFriends)
        {
            Regex regexUrl = new Regex(@"https://vk.com/(id[0-9]+|[a-z]+)");
            Regex regexId = new Regex(@"id[0-9]+$");
            Regex regexIdWithMask = new Regex(@"m/[a-z]+$");
            List<Person> persons = new List<Person>();
            persons.Add(new Person());
            
            if (regexUrl.IsMatch(userUrl))
            {
                if (regexIdWithMask.IsMatch(userUrl))
                {
                    string mask=regexIdWithMask.Match(userUrl).Value.Remove(0, 2);
                    persons[0].UID = DownloadUsers.GetUserId(mask);
                }
                else
                    persons[0].UID = Int32.Parse(regexId.Match(userUrl).Value.Remove(0, 2));
                persons[0] = DownloadUsers.DownloadUserInformation(persons[0].UID);
                if (addFriends)
                {
                    persons.AddRange(DownloadUsers.DownloadFriends(persons[0].UID));
                }
                foreach (var person in persons)
                {
                    Person personFromDb = db.People.FirstOrDefault(x => x.UID == person.UID);
                    if (personFromDb == null)
                    {
                        db.People.Add(DownloadUsers.DownloadUserInformation(person.UID));
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }
       // [HttpPost]
        public ActionResult Insert()
        {
            String urlGetFriends = "https://api.vkontakte.ru/method/friends.get?user_id=" + "72813887&fields=nickname";//Program.UserId; 
            List<Person> persons;
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            String jsonStringFriends = client.DownloadString(urlGetFriends);
            if (jsonStringFriends.Contains("error")) return null;
            persons = JsonConvert.DeserializeObject<Persons>(jsonStringFriends).People;
            //foreach (var person in persons)
            //{
            //    db.People.Add(person);
            //}
            //db.SaveChanges();



            return View();
        }
        //
        // GET: /People/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /People/Edit/5

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //
        // GET: /People/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /People/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}