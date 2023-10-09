using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Catalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; } 
        public string? Image { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
