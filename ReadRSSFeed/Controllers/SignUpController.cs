using ReadRSSFeed.BLL;
using ReadRSSFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ReadRSSFeed.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        UserBLL userBLL;
        public SignUpController()
        {
            this.userBLL = new UserBLL();
        }
        public ActionResult SignUp()
        {
            if (Session["UserId"] == null)
            {
                return View();
            }
            string Id = Session["UserId"].ToString();
            User user = userBLL.GetUserById(Id);
            // if user==null go to error page
            return View(user);
        }

        [HttpPost]
        public ActionResult InsertUserData(User user)
        {
            
            this.userBLL.InsertUser(user);

            // got book
            // what need i do with the book
            // update insert
            SendEmail(user);
            return RedirectToAction("WaitingForApproval", "WaitingForApproval");

        }
        public ActionResult UpdateUserData(User user)
        {
            user.Id = TempData["UserId"].ToString();
            this.userBLL.UpdateUser(user);

            // got book
            // what need i do with the book
            // update insert
            return RedirectToAction("SignUp", "SignUp");

        }
        public ActionResult DeleteUser(User user)
        {
            user.Id = TempData["UserId"].ToString();
            this.userBLL.DeleteUser(user);
            return RedirectToAction("SignUp", "SignUp");

        }

        [HttpPost]
        public ActionResult SendEmail(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("ofeksproject@gmail.com", "Ofek");
                    var receiverEmail = new MailAddress("Ofekiko00@gmail.com", "Receiver");
                    var password = "ProjectPassword";
                    var sub = "Approval";
                    var body = "user " +user.First_Name+ " "+user.Last_Name+ " is waiting for your approval";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }


    }
}