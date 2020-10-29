using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReadRSSFeed.Models;
using ReadRSSFeed.BLL;

namespace ReadRSSFeed.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            if (TempData["error"] != null)
                ViewBag.Error = true;
            if (TempData["trick"] != null)
                ViewBag.Trick = true;
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(BaseUser baseUser)
        {
            LoginBLL loginBLL = new LoginBLL();
            bool exist = loginBLL.IfUserExist(baseUser);
            if (exist == true)
            {
                // go to calculator

                Session["user"] = baseUser.NickName;
                Session["IsAuthenticated"] = true;
                return RedirectToAction("Index", "RSSFeed");
            }
            else
            {
                // go to login page 
                TempData["error"] = true;
                Session["IsAuthenticated"] = false;
                return RedirectToAction("login", "login");
            }

        }
    }
}