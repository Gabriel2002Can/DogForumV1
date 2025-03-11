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

            var discussions = await _context.Discussions.Include(d => d.User).Include(d => d.Comments).OrderByDescending(d => d.CreateDate).ToListAsync();

            
            return View(discussions);
        }

        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            //Reference https://stackoverflow.com/questions/59647805/setting-viewmodel-properties-from-model-class

            var discussions = await _context.Discussions
            .Include(d => d.User)
            .Where(d => d.UserId == user.Id)
            .OrderByDescending(d => d.CreateDate)
            .ToListAsync();

            var profileViewModel = new ProfileViewModel
            {
                User = user,
                Discussions = discussions
            };

            return View(profileViewModel);

        }

        public async Task<IActionResult> DiscussionsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get discussions by id
            var discussions = await _context.Discussions.Include(d => d.User).Include(m => m.Comments).ThenInclude(c => c.User).FirstOrDefaultAsync(m => m.DiscussionsId == id);

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
