using System.ComponentModel.DataAnnotations;

namespace Product_Catalog.ViewModel
{
    public class RegisterUserVM
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Not Matched with Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string RoleName { get; set; }
    }
}
