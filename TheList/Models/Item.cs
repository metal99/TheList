using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheList.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string TODOItem { get; set; }
        public Boolean Completed { get; set; }
    }
}