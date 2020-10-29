using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using ReadRSSFeed.Models;


namespace ReadRSSFeed.DAL
{
    public class RSSDAL
    {
        //List<category>
        //List<item>

        public List<Category> GetItemCategory()
        {
            //connect to xml
            List<Category> list = new List<Category>();
            list.Add(new Category { CategoryId = "0" , CategoryName = "All posts"});
            list.Add(new Category { CategoryId = "1", CategoryName = "League of Legends" });
            list.Add(new Category { CategoryId = "2", CategoryName = "Patch Notes" });
            list.Add(new Category { CategoryId = "3", CategoryName = "WPW" });

            return list;
        }
        public void GetAllItems(string CategoryTerm)
        {
            // connect to DataBase
            DataSet RssDataSet = new DataSet();
            RssDataSet.ReadXml("https://www.nerfplz.com/feeds/posts/default");
        }
    }
}
