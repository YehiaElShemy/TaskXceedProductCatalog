using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Catalog.ViewModel;

namespace Product_Catalog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult Registration()
        {
            ViewBag.AllRoles = GetAllRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterUserVM newUser)
        {
            ViewBag.Roles = GetAllRoles();
            if (ModelState.IsValid)
            {
                IdentityUser AddUser = new IdentityUser();
                AddUser.UserName = newUser.UserName;
                AddUser.Email = newUser.Email;
                IdentityResult result = await userManager.CreateAsync(AddUser, newUser.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(AddUser, false);
                    if (newUser.RoleName == "Admin")
                        await userManager.AddToRoleAsync(AddUser, newUser.RoleName);
                    else
                        await userManager.AddToRoleAsync(AddUser, newUser.RoleName);

                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(newUser);
                }
            }
            else
            {
                return View(newUser);
            }
        }

        private List<SelectListItem> GetAllRoles()
        {
            var AllRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            return AllRoles;


        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = "/Home/Index")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserVM userLogin, string ReturnUrl = "/Home/Index")
        {
            if (ModelState.IsValid)
            {
                IdentityUser User = await userManager.FindByNameAsync(userLogin.UserName); //FindByEmailAsync();
                if (User != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(User, userLogin.Password, userLogin.IsPersisite, false); //create cookie
                    if (result.Succeeded)
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                        ModelState.AddModelError("Password", "UserName or Password Not Correct");
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password Not Correct");
                }
            }
            return View(userLogin);
        }

        public async Task<IActionResult> LogOut()
        {
            //var user = userManager.FindByIdAsync(User.Identity.Name);
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");

        }

    }
}
