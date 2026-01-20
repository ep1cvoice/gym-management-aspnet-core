using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GymApp.Models;
using GymApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using GymApp.Models.Enums;
using GymApp.Models.Factories;


namespace GymApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager,
            AppDbContext context,
            IPassFactory passFactory)
        {
            _userManager = userManager;
            _context = context;
            _passFactory = passFactory;
        }

        public async Task<IActionResult> Index(
            string section = "account",
            string? editSection = null)
        {
            ViewData["Section"] = section;
            ViewBag.EditSection = editSection;

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            var model = new EditProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber,
                DocumentNumber = user.DocumentNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Section"] = "account";
                ViewBag.EditSection = "profile";
                return View("Index", model);
            }


            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.DocumentNumber = model.DocumentNumber;

            // EMAIL = LOGIN
            if (user.Email != model.Email)
            {
                user.Email = model.Email;
                user.UserName = model.Email;

                await _userManager.UpdateNormalizedEmailAsync(user);
                await _userManager.UpdateNormalizedUserNameAsync(user);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "Profile updated successfully.";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewData["Section"] = "account";
            ViewBag.EditSection = "profile";
            return View("Index", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Section"] = "account";
                ViewBag.EditSection = "password";
                return View("Index", new EditProfileViewModel());
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var result = await _userManager.ChangePasswordAsync(
                user,
                model.CurrentPassword,
                model.NewPassword);

            if (result.Succeeded)
            {
                TempData["Success"] = "Hasło zostało zmienione.";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewData["Section"] = "account";
            ViewBag.EditSection = "password";
            return View("Index", new EditProfileViewModel());
        }

        private readonly AppDbContext _context;
        private readonly IPassFactory _passFactory;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyPass(PassType passType)
        {
            var user = await _userManager.Users
                .Include(u => u.ActivePass)
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            if (user == null)
                return RedirectToAction("Login", "Auth");

            // BLOKADA – tylko jeden karnet
            if (user.ActivePass != null)
            {
                TempData["Error"] = "Masz już aktywny karnet. Usuń go, aby kupić nowy.";
                return RedirectToAction("Index", new { section = "passes" });
            }

            // SIMPLE FACTORY
            var pass = _passFactory.Create(passType, user.Id);

            user.ActivePass = pass;

            _context.UserPasses.Add(pass);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Płatność zakończona sukcesem. Karnet został aktywowany.";

            return RedirectToAction("Index", new { section = "passes" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePass()
        {
            var user = await _userManager.Users
                .Include(u => u.ActivePass)
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

            if (user == null)
                return RedirectToAction("Login", "Auth");

            if (user.ActivePass == null)
            {
                TempData["Error"] = "Nie masz aktywnego karnetu.";
                return RedirectToAction("Index", new { section = "passes" });
            }

            _context.UserPasses.Remove(user.ActivePass);
            user.ActivePass = null;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Karnet został usunięty.";

            return RedirectToAction("Index", new { section = "passes" });
        }


    }

}
