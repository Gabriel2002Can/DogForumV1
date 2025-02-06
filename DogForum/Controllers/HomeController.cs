using System.Diagnostics;
using DogForum.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogForum.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetDiscussions(int id)
        {
            return View();
        }

    }
}
