using System.Diagnostics;
using DogForum.Models;
using Microsoft.AspNetCore.Mvc;
using DogForum.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            var discussions = await _context.Discussions.Include(d => d.Comments).OrderByDescending(d => d.CreateDate).ToListAsync();
            return View(discussions);
        }

        public async Task<IActionResult> DiscussionsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get discussions by id
            var discussions = await _context.Discussions.Include(m => m.Comments.OrderByDescending(d => d.CreateDate)).FirstOrDefaultAsync(m => m.DiscussionsId == id);

            if (discussions == null)
            {
                return NotFound();
            }

            return View(discussions);
        }

        public IActionResult CommentsPost(int? id)
        {
           
            ViewBag.DiscussionsId = id;
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
