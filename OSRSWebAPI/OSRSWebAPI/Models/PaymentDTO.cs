using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSRSWebAPI.Models
{
    public class PaymentDTO
    {
        public string nameOnCard { get; set; }
        public int cardNumber { get; set; }
        public int cvv { get; set; }
        public string netBankingName { get; set; }

        public int order_id { get; set; }

        public int userid { get; set; }
        public DateTime expiryDate{ get; set; }
        public float amount { get; set; }
    }
}