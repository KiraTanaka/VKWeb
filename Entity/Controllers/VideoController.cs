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
        public ActionResult PopularVideo(string calendarfirst = null, string calendarlast= null)
        {
            DateTime dayFirst = new DateTime();
            DateTime dayLast = new DateTime();
            if (calendarfirst == null)
                dayFirst = DateTime.Now.Date;
            else
                dayFirst = DateTime.Parse(calendarfirst);
            if (calendarlast == null)
                dayLast = DateTime.Now;
            else
                dayLast = DateTime.Parse(calendarlast);
            Video popularVideo = db.PopularVideo.Where(x =>
                                DateTime.Compare(x.DateTime,dayFirst)>=0
                                && DateTime.Compare(dayLast, x.DateTime) >= 0).OrderByDescending(x => x.Views).First();
            ViewBag.PopularVideoPlayer = popularVideo.Player;
            return View(popularVideo);
        }
        public ActionResult Top10Video(string calendarfirst = null, string calendarlast = null)
        {
            DateTime dayFirst = new DateTime();
            DateTime dayLast = new DateTime();
            if (calendarfirst == null)
                dayFirst = DateTime.Now.Date;
            else
                dayFirst = DateTime.Parse(calendarfirst);
            if (calendarlast == null)
                dayLast = DateTime.Now;
            else
                dayLast = DateTime.Parse(calendarlast);
            List<Video> topVideo = db.PopularVideo.Where(x =>
                                    DateTime.Compare(x.DateTime,dayFirst)>=0
                                    && DateTime.Compare(dayLast, x.DateTime) >= 0).OrderByDescending(x => x.Views).Take(10).ToList();
            return View(topVideo);
        }
    }
}
