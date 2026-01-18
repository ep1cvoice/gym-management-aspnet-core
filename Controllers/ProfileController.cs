using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index(string section = "account", string? editSection = null)
        {
            ViewBag.Section = section;
            ViewBag.EditSection = editSection; 

            return View();
        }



    }
}
