using Product_Catalog.Models;

namespace Product_Catalog.Repository
{
    public interface IProduct
    {
        List<Category> GetCategories();

        string UploadImage(IFormFile image);
    }
}
