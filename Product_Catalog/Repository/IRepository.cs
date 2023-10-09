using Product_Catalog.Models;

namespace Product_Catalog.Repository
{
    public interface IRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int Id);
        Product GetByName(string name);
        void Add(Product newProduct);
        void Update(int Id,Product editProduct);
        void Delete(int Id);
        void SaveChanges();
        
    }
}
