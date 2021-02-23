using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadRSSFeed.Models
{
    public class User : BaseModel
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Birth_Year { get; set; }
        public string Password { get; set; }
        public string Rank { get; set; }
        public string Lane { get; set; }
        public string Summoner_Name { get; set; }
        public bool Approved { get; set; }
        public bool Admin { get; set; }

        public void UserFill()
        {
            this.First_Name = "";

            this.Last_Name = "";

            this.Email = "";

            this.Birth_Year = "";

            this.Password = "";

            this.Rank = "";

            this.Lane = "";

            this.Summoner_Name = "";


        }
     }
}