using ReadRSSFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ReadRSSFeed.Controllers
{
    public class RSSFeedController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string RSSURL)
        {
            WebClient wc = new WebClient();
            string rss = wc.DownloadString("https://www.nerfplz.com/feeds/posts/default");
            XNamespace atom = "http://www.w3.org/2005/Atom";
            XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");
            XDocument xml = XDocument.Parse(rss);

            var RSSFeedData = (from q in xml.Descendants(atom + "entry")
                              select new RSSFeed
                              {
                                  Title = q.Element(atom + "title").Value,
                                  Published = DateTime.Parse(q.Element(atom + "published").Value).ToString("dd/MM/yyyy HH:mm:ss"),
                                  Url = q.Descendants(atom + "link").Attributes("href").LastOrDefault().Value,
                                  Image = q.Element(media + "thumbnail") != null ? q.Element(media + "thumbnail").Attribute("url").Value : ""
                              }).Take(10);

            ViewBag.RSSFeed = RSSFeedData;
            ViewBag.URL = RSSURL;
            return View();
        }
    }
}