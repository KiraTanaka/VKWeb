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
using Apache.NMS;
using Apache.NMS.Util;
using Xstream.Core;
//using VKUsers;

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
            //send message
            UserInformationToAdd information = new UserInformationToAdd();
            information.userUrl = userUrl;
            information.addFriends = addFriends;            
          
            IConnectionFactory factory = new NMSConnectionFactory("tcp://localhost:61616");
            IConnection connection = factory.CreateConnection();
            connection = factory.CreateConnection();
            connection.Start();
            ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            IDestination QueueDestination = SessionUtil.GetDestination(session,"Users");
            IMessageProducer MessageProducer = session.CreateProducer(QueueDestination);
            //IObjectMessage objMessage = session.CreateObjectMessage(information);
            //XStream xstream = new XStream();
            //String xml = xstream.ToXml(information);
            string jsonString = JsonConvert.SerializeObject(information);
            ITextMessage textMessage = session.CreateTextMessage(jsonString);
            MessageProducer.Send(textMessage);
            session.Close();
            connection.Stop();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            Entity.Models.Person person = db.People.Find(id);
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