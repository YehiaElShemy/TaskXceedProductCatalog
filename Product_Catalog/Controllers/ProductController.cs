using Microsoft.AspNetCore.Mvc;
using Product_Catalog.Models;
using Product_Catalog.Repository;
using Product_Catalog.ViewModel;

namespace Product_Catalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct repoProduct;
        private readonly IRepository repository;

        public ProductController(IProduct _repoProduct,IRepository _repository )
        {
            repoProduct = _repoProduct;
            repository = _repository;
        }
        public IActionResult FilterByCategory(int Id)
        {
          List<GetAllProductWithCategoryNameVM> SelectProduct=repoProduct.GetProductsByCategoryId( Id );
            ViewBag.Categories= repoProduct.GetCategories();
            return View("Index", SelectProduct);
        }
        public IActionResult Index()
        {
            ViewBag.Categories= repoProduct.GetCategories();
            return View(repoProduct.getAllProductWithCategoryNames());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories= repoProduct.GetCategories();
            return View();
        }

        [HttpPost]
         //[AutoValidateAntiforgeryToken]
        public IActionResult Create(Product newProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string ImageName = repoProduct.UploadImage(newProduct.ImageFile);
                    newProduct.Image= ImageName;
                    repository.Add(newProduct);
                    repository.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            ViewBag.Categories = repoProduct.GetCategories();
            return View(newProduct);
        }
    }
}
