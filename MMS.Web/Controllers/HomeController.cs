using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MMS.Web.Models;

namespace MMS.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        var about = new AboutViewModel {
            Title = "About us",
            Message = "Welcome to our Movie Management System! We're dedicated to providing movie enthusiasts with a seamless platform to explore, discover, and review their favorite films. Our mission is to create acommunity where users can engage with each other, share their thoughts, and stay updated on the latest in the world of cinema. Share your opinions, rate movies, and engage in lively debates with fellow movie buffs. Your voice matters, and we believe in fostering a community where every opinion is respected and valued.",
            ContactUs = "Have a question, suggestion, or just want to get in touch? Feel free to reach out to us at admin@mail.com. We're always here to assist you and welcome any feedback you may have. Let's make our movie-watching experience even better, together!",
            Formed = new DateTime(2024,03,12)
        };
        return View(about);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
