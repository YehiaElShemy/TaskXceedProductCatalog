using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Product_Catalog.Models.Context
{
    public class Context:IdentityDbContext
    {
        public Context() : base() { }
       
        public Context(DbContextOptions options):base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category {Id=1,Name="Phones"},
                new Category {Id=2,Name = "Cars"},
                new Category {Id=3,Name = "laptops"}
                );
        }

    }
}
