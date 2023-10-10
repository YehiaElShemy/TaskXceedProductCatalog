using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Catalog.ViewModel
{
    public class GetAllProductWithCategoryNameVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
    }
}
