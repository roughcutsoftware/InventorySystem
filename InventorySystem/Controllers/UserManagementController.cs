using Microsoft.AspNetCore.Mvc;
using InventorySystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InventorySystem.web.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        // Replace with your seeded admin email
        private const string SeededAdminEmail = "admin1@sys.com";

        public UserManagementController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // 🧩 Prevent deletion of the seeded admin
            if (user.Email == SeededAdminEmail)
            {
                TempData["AdminErrorMessage"] = "You cannot delete the main system administrator.";
                return RedirectToAction("Index");
            }

            // 🧩 Prevent users from deleting themselves
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && currentUser.Id == user.Id)
            {
                TempData["UserErrorMessage"] = "You cannot delete your own account.";
                return RedirectToAction("Index");
            }

            await _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = "User deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}
