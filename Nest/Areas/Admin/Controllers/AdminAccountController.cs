using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nest.Areas.Admin.ViewModels;
using Nest.Models;
using Nest.ViewModels;

namespace Nest.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[RedirectIfInAdminAreaAttribute]
    public class AdminAccountController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;

        public AdminAccountController(UserManager<AppUser> userManager,
                                      RoleManager<IdentityRole> roleManager,
                                      SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var model = new AuthorizationVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(AuthorizationVM authorization)
        {
            string correctCode = "12345"; // Replace with your actual authorization code from a secure source

            if (authorization.Code == null || authorization.Code != correctCode)
            {
                ModelState.AddModelError("", "Authorization code is incorrect!");
                return View();
            }

            return Redirect("Login");
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login( string ReturnUrl, AdminLoginVM LoginVM)
        {
            var user = await _userManager.FindByNameAsync(LoginVM.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(LoginVM.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username or Email  already exist");
                    return View();
                }
            }

            var roleResult = await _userManager.IsInRoleAsync(user, "Admin");
            if (!roleResult)
            {
                ModelState.AddModelError("", "Username or email address not found.");
                return View();
            }


            var time = TimeZone.CurrentTimeZone;

            var result = await _signInManager.PasswordSignInAsync(user, LoginVM.Password, LoginVM.RememberMe, true);
            if (!result.IsLockedOut && user.LockoutEnd.HasValue)
            {
                ModelState.AddModelError("", "Wait until " + user.LockoutEnd.Value.AddHours(4).ToString("HH:mm:ss"));
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username,Email or Password is wrong");
                return View();

            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult >Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("Login");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AdminRegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var userEmail = await _userManager.FindByEmailAsync(registerVM.Email);
            if (userEmail != null)
            {
                ModelState.AddModelError("", "Email  already exist");
                return View(registerVM);
            }
            AppUser user = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                FullName = $"{registerVM.Name} {registerVM.Surname}",
                UserName = registerVM.Username,
                Email = registerVM.Email,
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", $"{error.Code} - {error.Description}");
                }
                return View(registerVM);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Admin");

            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            return RedirectToAction("Login");
        }
    }
}
