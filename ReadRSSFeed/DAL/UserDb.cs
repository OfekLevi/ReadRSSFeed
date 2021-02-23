using ReadRSSFeed.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ReadRSSFeed.BLL;
using ReadRSSFeed.DAL;

namespace ReadRSSFeed.DAL
{
    public class UserDb:BaseDb
    {
        protected override BaseModel CreateModel()
        {
            return new User();
        }

        protected override void FillDataModel(BaseModel baseModel, OleDbDataReader reader)
        {
            User user = baseModel as User;
            user.Id = reader["UserId"].ToString();
            user.First_Name = reader["First_Name"].ToString();
            user.Last_Name = reader["Last_Name"].ToString();
            user.Email = reader["Email"].ToString();
            user.Birth_Year = reader["Birth_Year"].ToString();
            user.Password= reader["Password"].ToString();
            user.Rank = reader["Rank"].ToString();
            user.Lane = reader["Lane"].ToString();
            user.Summoner_Name = reader["Summoner_Name"].ToString();
            user.Approved = bool.Parse(reader["Approved"].ToString());
            user.Admin = bool.Parse(reader["Admin"].ToString());



        }

        public List<User> GetAllUsers()
        {
            string sql = "Select * from [User]";
            return this.Select(sql).Cast<User>().ToList();
        }

        public User GetUserbyId(string userId)
        {
            string sql = "Select * from [User] where [UserId]=" + userId ;
            List<BaseModel> list = this.Select(sql);
            if (list == null || list.Count == 0)
                return null;
            return list[0] as User;
        }

        public User GetUserbyEmail(string userEmail)
        {
            
            string sql = "Select * from [User] where [Email]=" + "'" +userEmail+"'";
            
            List<BaseModel> list = this.Select(sql);
            if (list == null || list.Count == 0)
                return null;
            return list[0] as User;
        }

        public bool InsertNewUser(User user)
        {
            return this.ChangeData(user, SqlType.Insert);
        }

        public bool UpdateUser(User user)
        {
            return this.ChangeData(user, SqlType.Update);
        }

        public bool DeleteUser(User user)
        {
            return this.ChangeData(user, SqlType.Delete);
        }



        protected override string CreateUpdateSql(BaseModel baseModel)
        {
            User user = baseModel as User;        


            string sql = string.Format("Update [User] set First_Name='{1}', Last_Name='{2}', [Email]='{3}',Birth_Year='{4}',[Password]='{5}',Rank='{6}',Lane='{7}',Summoner_Name='{8}'  where UserId={0}",
                                         int.Parse(user.Id), user.First_Name, user.Last_Name, user.Email, user.Birth_Year, user.Password, user.Rank, user.Lane, user.Summoner_Name);
            return sql;
        }

        //First_Name
        //Last_Name
        //Email
        //    Birth_year
        //    Password
        //    Rank
        //    Lane
        //    Summoner_Name
        //    Approved
        //    Admin
        protected override string CreateInsertSql(BaseModel baseModel)
        {
            User user = baseModel as User;
            string sql = string.Format("Insert into [User](First_Name, Last_Name, [Email] , Birth_Year,[Password],Rank,Lane,Summoner_Name,Approved,[Admin] ) values ('{0}', '{1}', '{2}','{3}','{4}','{5}','{6}','{7}',{8},{9})",
                                        user.First_Name, user.Last_Name, user.Email, user.Birth_Year, user.Password, user.Rank, user.Lane, user.Summoner_Name, user.Approved, user.Admin);
            return sql;

        }

        protected override string CreateDeleteSql(BaseModel baseModel)
        {
            User user = baseModel as User;
            string sql = "Delete from [User] where UserId=" + user.Id;
            return sql;
        }

    }
}