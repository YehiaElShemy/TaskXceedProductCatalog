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

    }
}
