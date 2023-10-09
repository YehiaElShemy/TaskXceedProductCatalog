using Microsoft.AspNetCore.Mvc;
using Product_Catalog.Repository;

namespace Product_Catalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository repository;

        public ProductController(ProductRepository _repository)
        {
            repository = _repository;
        }
        public IActionResult Index()
        {
            ViewBag.Categories=repository.GetCategories();
            return View(repository.GetAll());
        }
    }
}
