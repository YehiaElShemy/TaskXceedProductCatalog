using Microsoft.EntityFrameworkCore;
using Product_Catalog.Models;
using Product_Catalog.Models.Context;
using Product_Catalog.ViewModel;
using System.Security.Claims;

namespace Product_Catalog.Repository
{
    public class ProductRepository : IRepository, IProduct
    {
        private readonly Context db;
        private readonly IWebHostEnvironment webHost;
        public ProductRepository(Context _db, IWebHostEnvironment _webHost)
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
        public void Update(Product editProduct)
        {
            db.Entry(editProduct).State = EntityState.Modified;
            db.Products.Update(editProduct);
        }
        //get product with no tracking
        public Product GetProductAsNoTracking(int Id)
        {
            return db.Products.AsNoTracking().FirstOrDefault(p => p.Id == Id);
        }

        public void Delete(int Id)
        {
            Product deleteProduct = GetById(Id);
            if (deleteProduct != null)
            {
                db.Products.Remove(deleteProduct);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.ToList();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public string UploadImage(IFormFile image) //method to upload image in my site
        {
            string uploadsFolder = Path.Combine(webHost.WebRootPath, "Images");
            string ImageName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, ImageName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return ImageName;
        }
        public List<Category> GetCategories() //method to return all Category
        {
            return db.Categories.ToList();
        }
        //return all products, Categories Name, who created this product 
        public List<GetAllProductWithCategoryNameVM> getAllProductWithCategoryNames()
        {
            var AllProductsWithGategoryname = db.Products.Include(p => p.Category).
                Include(p => p.User).ToList();
            if (AllProductsWithGategoryname != null)
            {
                List<GetAllProductWithCategoryNameVM> productsWithCategoryNameVMs = new List<GetAllProductWithCategoryNameVM>();
                foreach (var product in AllProductsWithGategoryname)
                {
                    productsWithCategoryNameVMs.Add(new GetAllProductWithCategoryNameVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        CreationDate = product.CreationDate,
                        StartDate = product.StartDate,
                        Duration = product.Duration,
                        Image = product.Image,
                        Price = product.Price,
                        CategoryName = product.Category.Name,
                        UserName = product.User.UserName
                    });
                }
                return productsWithCategoryNameVMs;
            }
            return new List<GetAllProductWithCategoryNameVM>();
        }

        //return all products, filter by select Category Name for admin
        public List<GetAllProductWithCategoryNameVM> GetProductsByCategoryId(int Id)
        {
            var AllProductByCateID = db.Products.Include(p => p.Category).Where(p => p.CategoryId == Id).ToList();
            if (AllProductByCateID != null)
            {
                List<GetAllProductWithCategoryNameVM> productsWithCateIdVMs = new List<GetAllProductWithCategoryNameVM>();
                foreach (var product in AllProductByCateID)
                {
                    productsWithCateIdVMs.Add(new GetAllProductWithCategoryNameVM
                    {
                        Id = product.Id,
                        Name = product.Name,
                        CreationDate = product.CreationDate,
                        Duration = product.Duration,
                        Image = product.Image,
                        Price = product.Price,
                        CategoryName = product.Category.Name
                    });
                }
                return productsWithCateIdVMs;
            }
            return new List<GetAllProductWithCategoryNameVM>();
        }

        //show all product in specific time 
        public List<Product> ShowProductsinSpecificTime()
        {
            var Allproducts = GetAll();
            if (Allproducts != null)
            {
                List<Product> products = new List<Product>();
                foreach (var product in Allproducts)
                {
                    DateTime EndDate = product.StartDate.AddDays(product.Duration);
                    if (product.StartDate <= DateTime.Now && EndDate > DateTime.Now)
                    {
                        products.Add(product);
                    }
                }
                return products;
            }
            return new List<Product>();
        }

        //return all products, filter by select Category Name for user


        public List<Product> GetAllProductsFillterbyCategoryIdforuser(int Id)
        {
            var AllProductByCateID = db.Products.Include(p => p.Category).Where(p => p.CategoryId == Id).ToList();
            if (AllProductByCateID != null)
            {
                List<Product> productsWithCateIdVMs = new List<Product>();
                foreach (var product in AllProductByCateID)
                {
                    DateTime EndDate = product.StartDate.AddDays(product.Duration);
                    if (product.StartDate <= DateTime.Now && EndDate > DateTime.Now)
                    {
                        productsWithCateIdVMs.Add(new Product
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Image = product.Image,
                            Price = product.Price,
                        });
                    }
                }
                return AllProductByCateID;
            }
            return new List<Product>();
        }
    }
}
