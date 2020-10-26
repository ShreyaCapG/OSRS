using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSRSClientSide.Models
{
    public class PaymentDTO
    {
       
        [RegularExpression(@"^[a-zA-Z]+$",
         ErrorMessage = "Name contains only alphabets.")]
        [Display(Name = "Name on card")]

        [StringLength(20, ErrorMessage = "Name must not be more than 20 character")]
        public string nameOnCard { get; set; }

        [Range(1000,9999,ErrorMessage = "Value must be between 1000 to 9999")]
        [Display(Name = "Card Number")]
        public int? cardNumber { get; set; }

        [Range(100, 999, ErrorMessage = "Value must be between 1000 to 9999")]
        [Display(Name = "CVV")]
        public int? cvv { get; set; }
        [Display(Name = "Select bank for netbanking")]
        public string netBankingName { get; set; }

        [ScaffoldColumn(false)]
        public int order_id { get; set; }

        [ScaffoldColumn(false)]
        public int userid { get; set; }
        [DataType(DataType.Date)]
        public DateTime? expiryDate { get; set; }
        public double amount { get; set; }
    }
}