using System.ComponentModel.DataAnnotations;

namespace OSRSClientSide.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Select Usertype")]
        [Display(Name = "Usertype")]
        public EnumsAndConstants.Usertype usertype { get; set; }
        
    }
}