using ReadRSSFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ReadRSSFeed.Controllers;

namespace ReadRSSFeed.Controllers
{
    public class RSSFeedController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserId"] == null)               
            {
                
                bool a = string.IsNullOrEmpty(Session["UserId"] as string);
                return RedirectToAction("login", "login");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Index(string RSSURL , string CategoryTerm)
        {            
           
            
                WebClient wc = new WebClient();
                string rss = wc.DownloadString("https://www.nerfplz.com/feeds/posts/default");
                XNamespace atom = "http://www.w3.org/2005/Atom";
                XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");
                XNamespace scheme = XNamespace.Get("http://www.blogger.com/atom/ns#");
                XDocument xml = XDocument.Parse(rss);

                var CT = CategoryTerm == "" ? "All Posts" : CategoryTerm;

                var RSSFeedDataAll = from q in xml.Descendants(atom + "entry")
                                     where q.Element(atom + "category") != null
                                     select new RSSFeed
                                     {
                                         Title = q.Element(atom + "title").Value,
                                         Published = DateTime.Parse(q.Element(atom + "published").Value).ToString("dd/MM/yyyy HH:mm:ss"),
                                         Url = q.Descendants(atom + "link").Attributes("href").LastOrDefault().Value,
                                         Image = q.Element(media + "thumbnail") != null ? q.Element(media + "thumbnail").Attribute("url").Value : "",
                                         Category = q.Element(atom + "category").Attribute("term").Value
                                     };

                var RSSFeedDataTerm = from q in xml.Descendants(atom + "entry")
                                      where q.Element(atom + "category").Attribute("term").Value == CT && q.Element(atom + "category") != null
                                      select new RSSFeed
                                      {
                                          Title = q.Element(atom + "title").Value,
                                          Published = DateTime.Parse(q.Element(atom + "published").Value).ToString("dd/MM/yyyy HH:mm:ss"),
                                          Url = q.Descendants(atom + "link").Attributes("href").LastOrDefault().Value,
                                          Image = q.Element(media + "thumbnail") != null ? q.Element(media + "thumbnail").Attribute("url").Value : "",
                                          Category = q.Element(atom + "category").Attribute("term").Value
                                      };

                var RSSFeedData = CategoryTerm == "All Posts" ? RSSFeedDataAll : RSSFeedDataTerm;

                ViewBag.RSSFeed = RSSFeedData;
                ViewBag.URL = RSSURL;
                return View();
            
            
        }

    }
}