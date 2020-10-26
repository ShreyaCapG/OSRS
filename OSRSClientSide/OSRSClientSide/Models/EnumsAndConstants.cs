using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSRSClientSide.Models
{
    public class EnumsAndConstants
    {
        public enum Usertype
        {
            seller = 1,
            customer = 2
        }
        public enum Banks
        {
            AXIS,
            HDFC,
            SBI,
            ICICI
        }
    }
}