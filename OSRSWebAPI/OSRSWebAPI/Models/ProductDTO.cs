using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSRSWebAPI.Models
{
    public class ProductDTO
    {
       
       
        public int product_id { get; set; }

        
        public int userid { get; set; }

       
        public string product_name { get; set; }


        public double price { get; set; }

        
        public string product_category { get; set; }
    }
}