using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Win32;
using Nest.Models;
using Nest.ViewModels;

namespace Nest.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Login()
        {
            ViewBag.PreloaderPartialView = true;
            ViewBag.MobileHeaderPartialView = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string ReturnUrl, LoginVM loginVM)
        {
            ViewBag.PreloaderPartialView = true;
            ViewBag.MobileHeaderPartialView = true;

            //if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
                if (user != null)
                {
                    ModelState.AddModelError("", "Email  already exist");
                    return View();
                }
            }

            var time = TimeZone.CurrentTimeZone;

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);
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
            return RedirectToAction("Index", "Home");
        }


        public async  Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Register()
        {
            ViewBag.PreloaderPartialView = true;
            ViewBag.MobileHeaderPartialView = true;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            ViewBag.PreloaderPartialView = true;
            ViewBag.MobileHeaderPartialView = true;
            if (!ModelState.IsValid) return View(registerVM);

            var userName = await _userManager.FindByNameAsync(registerVM.Username);
            if (userName == null)
            {
                var userEmail = await _userManager.FindByEmailAsync(registerVM.Email);
                if (userEmail != null)
                {
                    ModelState.AddModelError("", "Email  already exist");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Username  already exist");
                return View();
            }

            AppUser user = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                FullName = $"{registerVM.Name} {registerVM.Surname}",
                Email = registerVM.Email,
                UserName = registerVM.Username,
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

                var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

            if (registerVM.IsVendor)
            {
                 roleResult = await _userManager.AddToRoleAsync(user, "Vendor");
            }
            else
            {
                 roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            }

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

        public async Task<IActionResult> PrivacyPolicy()
        {
            ViewBag.PreloaderPartialView = true;
            ViewBag.MobileHeaderPartialView = true;
            return View();
        }

        //public async Task CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Vendor" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Customer" });
        //}

    }
}
