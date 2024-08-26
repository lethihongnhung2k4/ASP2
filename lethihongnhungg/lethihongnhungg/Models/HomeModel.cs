using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lethihongnhungg.DB;

namespace lethihongnhungg.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<DB.Category> ListCategory { get; set; }
    }
}