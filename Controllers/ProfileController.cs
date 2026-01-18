using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GymApp.Models;
using GymApp.Models.ViewModels;

namespace GymApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

    }

}
