using Product_Catalog.Models;
using Product_Catalog.ViewModel;

namespace Product_Catalog.Repository
{
    public interface IProduct
    {
        List<Category> GetCategories();
        List<GetAllProductWithCategoryNameVM> getAllProductWithCategoryNames();
        List<GetAllProductWithCategoryNameVM> GetProductsByCategoryId(int Id);
        List<Product> GetAllProductsFillterbyCategoryIdforuser(int id);
        List<Product> ShowProductsinSpecificTime();
        public Product GetProductAsNoTracking(int Id);
        string UploadImage(IFormFile image);
    }
}
