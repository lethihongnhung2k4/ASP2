using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lethihongnhungg.DB;

namespace lethihongnhungg.Models
{
    public class Orderdetail
    {
        public long Id { get; set; }
        public long Order_id { get; set; }
        public long Product_id { get; set; }
        public Nullable<double> Price { get; set; }
        public int Qty { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<double> Amount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}