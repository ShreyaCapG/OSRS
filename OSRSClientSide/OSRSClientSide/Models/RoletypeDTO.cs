using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSRSClientSide.Models
{
    public class RoletypeDTO
    {
        
        public string user_type { get; set; }
        public int roleid { get; set; }
        public ICollection<UsertableDTO> UsertableDTOs { get; set; }
    }
}