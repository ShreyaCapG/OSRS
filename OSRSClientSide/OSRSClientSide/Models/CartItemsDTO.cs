using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSRSClientSide.Models
{
    public class CartItemsDTO
    {
        public string product_name;
        public double price;
        public string product_category;
        public int cartid;
        public int quantity;
        public int no_of_renting_days;
        public double total;
    }
}