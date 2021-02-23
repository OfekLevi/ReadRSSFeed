using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReadRSSFeed.Models;
using ReadRSSFeed.BLL;
using ReadRSSFeed.DAL;


namespace ReadRSSFeed.Controllers
{
    public class LoginController : Controller
    {
        UserBLL userBLL;
        UserDb userDb;
        // GET: Login
        public LoginController()
        {
            this.userBLL = new UserBLL();
            this.userDb = new UserDb();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (TempData["error"] != null)
                ViewBag.Error = true;
            if (TempData["NotApproved"] != null)
                ViewBag.NotApproved = true;
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(BaseUser baseUser)
        {

            LoginBLL loginBLL = new LoginBLL();
            bool exist = loginBLL.IfUserExist(baseUser);
            if (exist == true)
            {
                bool sess = true;
                Session["UserId"] = sess;
                return RedirectToAction("Login", "Login");
            }
            else
            {
                // go to login page 
                TempData["error"] = true;
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult CheckLogin_New(User user)
        {
            LoginBLL loginBLL = new LoginBLL();
            bool exist = loginBLL.IfUserExistNew(user);
            User Appr = userDb.GetUserbyEmail(user.Email);
            if (exist == true && Appr.Approved == true)
            {
                Session["UserId"] = Appr.Id;
                return RedirectToAction("Index", "RSSFeed");
            }
            if(user.Approved != true)
            {
                

                // go to login page 
                TempData["error"] = true;
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TempData["NotApproved"] = true;
                ViewBag.NotApproved = true;
                return RedirectToAction("Login", "Login");
            }
        }
    }
}