using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSRSClientSide.Models
{
    public class ProductDTO
    {
        [Display(Name = "Product ID")]
        public int product_id { get; set; }
        [ScaffoldColumn(false)]
        public int userid { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [Display(Name = "Product Name")]
        public string product_name { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price")]
        public double price { get; set; }

        [Required(ErrorMessage = "Product Category is required")]
        [Display(Name = "Product Category")]
        public string product_category { get; set; }
    }
}