using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FundManagementSystem.Models;
using System.Threading.Tasks;

namespace FundManagementSystem.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /UserProfile/Details
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            return View(user);
        }
    }
}
