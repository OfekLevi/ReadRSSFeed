using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReadRSSFeed.DAL;
using ReadRSSFeed.Models;


namespace ReadRSSFeed.BLL
{
    public class UserBLL
    {
        UserDb userDb;
        
        public UserBLL()
        {
            this.userDb = new UserDb();
        }

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

        public User GetUserById(string id)
        {
            return this.userDb.GetUserbyId(id);
        }


    }
}