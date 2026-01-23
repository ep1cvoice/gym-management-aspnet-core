using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GymApp.Models;
using GymApp.Models.ViewModels;


namespace GymApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult StartTraining()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth",
                    new { returnUrl = Url.Action("Index", "Profile") });
            }

            return RedirectToAction("Index", "Profile");
        }
        public IActionResult ChooseTrainer()
        {
            var targetUrl = Url.Action(
                "Index",
                "Profile",
                new { section = "personal" }
            );

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(
                    "Login",
                    "Auth",
                    new { returnUrl = targetUrl }
                );
            }

            return Redirect(targetUrl!);
        }
        public IActionResult ChooseMembership()
        {
            var targetUrl = Url.Action(
                "Index",
                "Profile",
                new { section = "passes" }
            );

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(
                    "Login",
                    "Auth",
                    new { returnUrl = targetUrl }
                );
            }

            return Redirect(targetUrl!);
        }




        // ---------------- REGISTER ----------------

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                TempData["Success"] = "Konto zostało pomyślnie utworzone. Teraz możesz się zalogowac.";
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }




        // -------- LOGIN GET --------
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // -------- LOGIN POST --------
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                TempData["Success"] = "Zostałeś pomyślnie zalogowany!";

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Wprowadzono błędne dane.");
            return View(model);
        }



        // ---------------- LOGOUT ----------------

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Info"] = "Zostałeś poprawnie wylogowany.";
            return RedirectToAction("Index", "Home");
        }
    }
}
