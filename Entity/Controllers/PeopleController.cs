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

namespace Entity.Controllers
{
    public class PeopleController : Controller
    {
        private EntityContext db = new EntityContext();

        //
        // GET: /People/

        public ActionResult Index()
        {
            return View(db.People.ToList());
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
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
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
            foreach (var person in persons)
            {
                db.People.Add(person);
            }
            db.SaveChanges();



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