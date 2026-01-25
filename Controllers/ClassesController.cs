using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymApp.Models;
using GymApp.Services;

namespace GymApp.Controllers
{
    [Authorize]
    public class ClassesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookingService _bookingService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClassesController(
            AppDbContext context,
            IBookingService bookingService,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _bookingService = bookingService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _context.TrainingClasses
                .Include(c => c.Trainer)
                .ToListAsync();

            return View(classes);
        }

        [HttpPost]
        public async Task<IActionResult> Book(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var success = await _bookingService.BookClassAsync(id, user.Id);

            TempData["Message"] = success
                ? "Zostałeś zapisany na zajęcia."
                : "Nie udało się zapisać (brak miejsc lub już zapisany).";

            return RedirectToAction(nameof(Index));
        }
    }
}
