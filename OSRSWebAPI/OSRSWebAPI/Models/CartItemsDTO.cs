using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSRSWebAPI.Models
{
    public class CartItemsDTO
    {
        //p.product_name, p.price, p.product_category, c.cart_id, c.quantity, c.no_of_renting_days, Total = p.price* c.quantity
        public string product_name;
        public double price;
       public string product_category;
        public int cartid;
        public int quantity;
        public int no_of_renting_days;
        public double total;
        //public int productid;

    }
}