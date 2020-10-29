using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReadRSSFeed.Models;

namespace ReadRSSFeed.BLL
{
   
        public class LoginBLL
        {

            public bool IfUserExist(BaseUser baseUser)
            {
                return baseUser.NickName == "ofek" && baseUser.Password == "1234";
                // connect to database to check if user exist
            }
        }
    
}