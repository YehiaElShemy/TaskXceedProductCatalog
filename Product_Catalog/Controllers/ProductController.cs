using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Catalog.Models;
using Product_Catalog.Repository;
using Product_Catalog.ViewModel;
using System.Security.Claims;

namespace Product_Catalog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProduct repoProduct;
        private readonly IRepository repository;

        public ProductController(IProduct _repoProduct, IRepository _repository)
        {
            repoProduct = _repoProduct;
            repository = _repository;
        }
        [AllowAnonymous]
        public IActionResult FilterByCategory(int Id) //filter category by id for admin
        {
            List<GetAllProductWithCategoryNameVM> SelectProduct = repoProduct.GetProductsByCategoryId(Id);
            ViewBag.Categories = repoProduct.GetCategories();
            return View("Index", SelectProduct);
        }
   
        public IActionResult Index() //show all product with category name
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
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//get user id who login now
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
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//get user id who login now
                    //get product with no tracking to avoid execption two Entity have same id  
                    Product oldproduct = repoProduct.GetProductAsNoTracking(Id);  

                    if (oldproduct != null)
                    {
                        if (newEdit.ImageFile == null)
                        {
                            newEdit.Image = oldproduct.Image;
                        }
                        else
                        {
                            string ImageName = repoProduct.UploadImage(newEdit.ImageFile);
                            newEdit.Image = ImageName;
                        }
                      
                        newEdit.UserId = userId;
                        repository.Update(newEdit);
                        repository.SaveChanges();
                        return RedirectToAction("Index");
                    }
               
                }
            }
            catch (Exception ex)
            {
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
        [AllowAnonymous]
        public IActionResult Details(int Id)
        {
            return View(repository.GetById(Id));
        }
        [AllowAnonymous]
        public IActionResult ShowProductsForUsers()
        {
            ViewBag.Categories = repoProduct.GetCategories();
            return View(repoProduct.ShowProductsinSpecificTime());
        }
        [AllowAnonymous]
        public IActionResult FilterByCategoryForUser(int Id) //filter category by id for user
        {
            List<Product> SelectProduct = repoProduct.GetAllProductsFillterbyCategoryIdforuser(Id);
            ViewBag.Categories = repoProduct.GetCategories();
            return View("ShowProductsForUsers", SelectProduct);
        }
        public bool CheckPrice(double Price)
        {
            if (Price >= 0)
                return true;
            return false;

        }
    }
}
