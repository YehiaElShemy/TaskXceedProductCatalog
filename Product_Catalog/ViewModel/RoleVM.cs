using System.ComponentModel.DataAnnotations;

namespace Product_Catalog.ViewModel
{
    public class RoleVM
    {
        [Display(Name ="Role Name") ,Required(ErrorMessage ="Enter Role Name")]
        public string RoleName { get; set; }
    }
}
