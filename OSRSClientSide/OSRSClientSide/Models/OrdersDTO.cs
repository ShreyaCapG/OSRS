using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSRSClientSide.Models
{
    public class OrdersDTO
    {
        public int order_id { get; set; }
        public int userid { get; set; }
        public double amount { get; set; }
    }
}