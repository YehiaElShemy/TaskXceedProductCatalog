using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Catalog.Models;
using Product_Catalog.Repository;
using Product_Catalog.ViewModel;
using System.Security.Claims;

namespace Product_Catalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct repoProduct;
        private readonly IRepository repository;

        public ProductController(IProduct _repoProduct, IRepository _repository)
        {
            repoProduct = _repoProduct;
            repository = _repository;
        }
        public IActionResult FilterByCategory(int Id)
        {
            List<GetAllProductWithCategoryNameVM> SelectProduct = repoProduct.GetProductsByCategoryId(Id);
            ViewBag.Categories = repoProduct.GetCategories();
            return View("Index", SelectProduct);
        }
        public IActionResult Index()
        {
            ViewBag.Categories = repoProduct.GetCategories();
            return View(repoProduct.getAllProductWithCategoryNames());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = repoProduct.GetCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product newProduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    
                    string ImageName = repoProduct.UploadImage(newProduct.ImageFile);
                    newProduct.Image = ImageName;
                    newProduct.UserId = userId;
                    repository.Add(newProduct);
                    repository.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            ViewBag.Categories = repoProduct.GetCategories();
            return View(newProduct);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            ViewBag.Categories = repoProduct.GetCategories();
            return View(repository.GetById(Id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, Product newEdit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string ImageName = repoProduct.UploadImage(newEdit.ImageFile);
                    newEdit.Image = ImageName;
                    repository.Update(Id,newEdit);
                    repository.SaveChanges();
                    return RedirectToAction("Index");


                }
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("", ex.Message);
                return View("Error", new ErrorViewModel { ErrorMessage = $"Internal server error {ex.Message}" });
            }
            ViewBag.Categories = repoProduct.GetCategories();
            return View(newEdit);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            repository.Delete(Id);
            repository.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Details(int Id)
        {
            return View(repository.GetById(Id));
        }

        public IActionResult ShowProductsForUsers()
        {
            return View(repoProduct.ShowProductsinSpecificTime());
        }
    }
}
