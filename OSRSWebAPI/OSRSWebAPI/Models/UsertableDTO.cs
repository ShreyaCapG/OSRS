﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSRSWebAPI.Models
{
    public class UsertableDTO
    {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string contact_number { get; set; }
        public int userid { get; set; }
        public RoletypeDTO usertype { get; set; }

       
    }
}