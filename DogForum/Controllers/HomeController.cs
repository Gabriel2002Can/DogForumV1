using System.Diagnostics;
using DogForum.Models;
using Microsoft.AspNetCore.Mvc;
using DogForum.Data;
using Microsoft.EntityFrameworkCore;

namespace DogForum.Controllers
{
    public class HomeController : Controller
    {

        private readonly DogForumContext _context;

        public HomeController(DogForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var discussions = await _context.Discussions.Include(m => m.Comments).ToListAsync();
            return View(discussions);
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
