using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity.Models;

namespace Entity.Controllers
{
    public class VideoController : Controller
    {
        private Context db = new Context();
        public ActionResult PopularVideo(string calendar = null)
        {
            DateTime day = new DateTime();
            if (calendar == null)
                day = DateTime.Now;
            else
                day = DateTime.Parse(calendar);
            Video popularVideo = db.PopularVideo.Where(x =>
                    x.DateTime.Day == day.Day
                    && x.DateTime.Month == day.Month
                    && x.DateTime.Year == day.Year).OrderBy(x => x.Id).First();
            ViewBag.PopularVideoPlayer = popularVideo.Player;
            return View(popularVideo);
        }
        public ActionResult Top10Video(string calendar = null)
        {
            DateTime day = new DateTime();
            if (calendar == null)
                day = DateTime.Now;
            else
                day = DateTime.Parse(calendar);
            List<Video> topVideo = db.PopularVideo.Where(x =>
                    x.DateTime.Day == day.Day
                    && x.DateTime.Month == day.Month
                    && x.DateTime.Year == day.Year).OrderBy(x => x.Id).ToList();
            return View(topVideo);
        }
    }
}
