using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSRSClientSide.Models
{
    public class UsertableView
    {
        [StringLength(30, ErrorMessage = "Username must not be more than 30 character")]
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z]+$",
         ErrorMessage = "Name contains only alphabets.")]
        [Display(Name = "Full Name")]
        public string name { get; set; }


        [StringLength(20, ErrorMessage = "Username must not be more than 20 character")]
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^[a-zA-Z]+$",
         ErrorMessage = "Username contains only alphabets.")]
        [Display(Name = "Username")]
        public string username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4,20}$",
        ErrorMessage = "Password must be more than 4 character, less than 20 character and minimum 1 number ")]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"^[6789]{1}[0-9]{9}$",
        ErrorMessage = "Contact Number contains only digits,starts with 6,7,8 or 9 and must be of length 10.")]
        [Display(Name = "Contact Number")]
        public string contact_number { get; set; }
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public int userid { get; set; }
        public RoletypeDTO usertype { get; set; }
    }
}