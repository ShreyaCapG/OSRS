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
        public string nameOnCard { get; set; }
        public int? cardNumber { get; set; }
        public int? cvv { get; set; }
        public string netBankingName { get; set; }

        [ScaffoldColumn(false)]
        public int order_id { get; set; }

        [ScaffoldColumn(false)]
        public int userid { get; set; }
        public DateTime? expiryDate { get; set; }
        public double amount { get; set; }
    }
}