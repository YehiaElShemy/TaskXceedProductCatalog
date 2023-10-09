using Product_Catalog.Models;
using Product_Catalog.Models.Context;

namespace Product_Catalog.Repository
{
    public class ProductRepository : IRepository,IProduct
    {
        private readonly Context db;
        private readonly IWebHostEnvironment webHost;

        public ProductRepository(Context _db,IWebHostEnvironment _webHost)
        {
            db = _db;
            this.webHost = _webHost;
        }
        public Product GetById(int Id)
        {
            return db.Products.Find(Id) ?? new Product();
        }

        public Product GetByName(string name)
        {
            return db.Products.FirstOrDefault(p => p.Name == name);
        }

        public void Add(Product newProduct)
        {
            db.Products.Add(newProduct);
        }
        public void Update(int Id, Product editProduct)
        {
            Product oldProduct = GetById(Id);
            if (oldProduct != null)
            {
                db.Products.Update(editProduct);
            }
        }

        public void Delete(int Id)
        {
            db.Products.Remove(GetById(Id));
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.ToList();//AsQueryable();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public string UploadImage(IFormFile image)
        {
           // string uploadsFolder = Path.Combine("wwwroot", "Images");
            string uploadsFolder = Path.Combine(webHost.ContentRootPath, "Images");
            string ImageName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, ImageName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return ImageName;

        }

        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }
    }
}
