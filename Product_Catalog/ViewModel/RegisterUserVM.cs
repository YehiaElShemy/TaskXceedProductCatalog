using System.ComponentModel.DataAnnotations;

namespace Product_Catalog.ViewModel
{
    public class RegisterUserVM
    {

        [Display(Name = "User Name"), Required(ErrorMessage = "Enter user name")]
        public string UserName { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Enter complex password")]
        public string Password { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Not Matched with Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string RoleName { get; set; }
    }
}
