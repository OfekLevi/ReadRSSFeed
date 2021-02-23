using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using ReadRSSFeed.Models;
using System.Configuration;

namespace ReadRSSFeed.DAL
{
    
    
      public enum SqlType { Select, Insert, Delete, Update }
      public abstract class BaseDb
      {
          OleDbConnection connection;// צינור למקור נתונים
          OleDbCommand command; // ברז על הצינור
          OleDbDataReader reader;// שומר נתונים

          SqlType sqlType = SqlType.Select;
        public BaseDb()
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Year12\ReadRSSFeed\ReadRSSFeed\App_Data\V5.mdb";
            this.connection = new OleDbConnection();
            this.connection.ConnectionString = connectionString;
            this.command = new OleDbCommand();
            this.command.Connection = this.connection;
        }

        protected List<BaseModel> Select(string sql)
        {
            this.command.CommandText = sql;
            List<BaseModel> list = new List<BaseModel>();
            try
            {
                this.connection.Open(); // Open Connection
                this.reader = this.command.ExecuteReader(); // reader with data (recordset)
                //1.  SQL is not correct //2. The table is open in edit mode
                while (this.reader.Read())
                {
                    BaseModel baseModel = this.CreateModel();// Create model (book)? (Author)?(city) ?
                    FillDataModel(baseModel, this.reader); // Fill data model 
                    list.Add(baseModel);// Add model into list
                }
                return list;
            }
            catch (Exception e)
            {
                string error = e.Message;
                return null;
            }
            finally
            {
                this.connection.Close();
            }

        }

        protected bool ChangeData(BaseModel baseModel, SqlType sqlType)
        {
            this.command.CommandText = CreateSql(baseModel, sqlType);
            try
            {
                this.connection.Open();
                return this.command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

            }
            finally
            {
                this.connection.Close();
            }
            return false;
            // SQL -- every object create sql 
        }

        private string CreateSql(BaseModel baseModel, SqlType sqlType)
        {
            if (sqlType == SqlType.Delete)
                return CreateDeleteSql(baseModel);
            else if (sqlType == SqlType.Insert)
                return CreateInsertSql(baseModel);
            else
                return CreateUpdateSql(baseModel);
        }

        protected abstract BaseModel CreateModel();
        protected abstract void FillDataModel(BaseModel baseModel, OleDbDataReader reader);

        protected abstract string CreateUpdateSql(BaseModel baseModel);
        protected abstract string CreateInsertSql(BaseModel baseModel);
        protected abstract string CreateDeleteSql(BaseModel baseModel);
    }
    
}