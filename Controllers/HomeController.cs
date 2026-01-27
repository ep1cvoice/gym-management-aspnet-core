using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GymApp.Models;
using GymApp.Models.Notifications;



namespace GymApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Pricing()
    {
        return View();
    }

    public class PersonalTrainingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult Subscribe(string email)
    {

        NotificationFactory factory = new EmailNotificationFactory();

        INotification notification = factory.CreateNotification();

        notification.Send(
            email,
            "Pomyślnie zapisano do newslettera. Dziękujemy!"
        );

        TempData["SuccessMessage"] = "Pomyślnie zapisano do newslettera.";

        return Redirect("/Home/Index#newsletter");

    }

}
