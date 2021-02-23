using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReadRSSFeed.DAL;
using ReadRSSFeed.Models;

namespace ReadRSSFeed.BLL
{
   
        public class LoginBLL
        {
            UserDb userDb;

          public LoginBLL()
          {
            this.userDb = new UserDb();
          }
          
          public bool IfUserExist(BaseUser baseUser)
          {
              return baseUser.NickName == "ofek" && baseUser.Password == "1234";
              // connect to database to check if user exist
          }            
          public bool IfUserExistNew (User user)
          {
            string UserEmail = (user.Email).ToString();
            string UserPassword = user.Password;
            User thisUser = userDb.GetUserbyEmail(UserEmail);
            if (thisUser == null)
            {
                return false;
            }
            if(UserPassword == thisUser.Password)
            {
                return true;
            }
            return false;


        }
          public User GetUserById(string id)
          {
              return this.userDb.GetUserbyId(id);          }

          public bool DeleteUser(User user)
          {
              return this.userDb.DeleteUser(user);
          }
          public bool InsertUser(User user)
          {
              return this.userDb.InsertNewUser(user);
          }
          public bool UpdateUser(User user)
          {
              return this.userDb.UpdateUser(user);
          }

        


    }
}