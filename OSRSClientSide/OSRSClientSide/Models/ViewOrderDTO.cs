using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSRSClientSide.Models
{
    public class ViewOrderDTO
    {
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Product Category")]
        public string product_category { get; set; }
        [Display(Name = "Product Name")]
        public string product_name { get; set; }
        [Display(Name = "Order ID")]
        public int order_id { get; set; }
        [Display(Name = "User ID")]

        public int userid { get; set; }
        [Display(Name = "Amount")]

        public double amount { get; set; }
        [Display(Name = "Order Date")]

        public Nullable<System.DateTime> order_date { get; set; }
        [Display(Name = "Shipping Date")]

        public Nullable<System.DateTime> shipping_date { get; set; }
    }
}