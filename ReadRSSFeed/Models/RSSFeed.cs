using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadRSSFeed.Models
{
    public class RSSFeed//item
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Published { get; set; }        
        public string Image { get; set; }
        public string Category { get; set; }
    }
}