using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index(string section = "account", bool edit = false)
        {
            ViewBag.Section = section;
            ViewBag.EditMode = edit;

            return View();
        }


    }
}
