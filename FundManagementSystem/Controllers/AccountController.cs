using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FundManagementSystem.Models; // Your ApplicationUser model namespace
using FundManagementSystem.ViewModels; // Your ViewModels namespace
using System.Threading.Tasks;

namespace FundManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        // GET: /Account/Register
        public IActionResult Register()
        {
            return View(new RegisterViewModel()); // ✅ Pass a valid model instance

        }

        // POST: /Account/Register

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Field: {modelState.Key}, Error: {error.ErrorMessage}");
                }
            }

            System.Diagnostics.Debug.WriteLine($"FullName: {model.FullName}, Email: {model.Email}");


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber
                    // Add NID and other properties if you add them to ApplicationUser
                };

                // Optionally, add NID if ApplicationUser has it:
                // user.NID = model.NID;

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Redirect to Login after successful registration
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }



        // GET: /Account/Login
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        // Optional: Logout action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
