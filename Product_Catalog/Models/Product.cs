using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Product_Catalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Product Name"), Required(ErrorMessage = "Enter name of product")]
        public string Name { get; set; }
        [Display(Name = "creation Date"), Required(ErrorMessage = "Enter Createion Date of product from today")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "start Date"), Required(ErrorMessage = "Enter start Date of product")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter number of Days show product")]
        public int Duration { get; set; }
        [Required(ErrorMessage = "Enter price of product")]
        [Remote("CheckPrice", "Product", ErrorMessage = "price must be greater than or equal 0")]
        public double Price { get; set; }
        [Display(Name = "choose Image")]
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
