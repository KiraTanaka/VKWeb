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
        private Context db = new Context();

        public PeopleController() { }
        public ActionResult Index()
        {          
            return View(db.People.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

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

        public ActionResult Delete(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}