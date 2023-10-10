using System.ComponentModel.DataAnnotations;

namespace Product_Catalog.ViewModel
{
    public class LoginUserVM
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersisite { get; set; }
    }
}
