using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSRSWebAPI.Models
{
    public class ViewOrderDTO
    {
        public string name { get; set; }
        public string product_category { get; set; }
        public string product_name { get; set; }
        public int order_id { get; set; }

        public int userid { get; set; }

        public double amount { get; set; }
        public Nullable<System.DateTime> order_date { get; set; }

        public Nullable<System.DateTime> shipping_date { get; set; }

    }
}