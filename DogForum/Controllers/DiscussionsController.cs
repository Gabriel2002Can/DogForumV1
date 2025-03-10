using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DogForum.Data;
using DogForum.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace DogForum.Controllers
{
    [Authorize]
    public class DiscussionsController : Controller
    {
        private readonly DogForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public DiscussionsController(DogForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Discussions
        public async Task<IActionResult> Index()
        {

            var discussions = await _context.Discussions
                .Include(d => d.User)
                .Include(d => d.Comments)
                .Where(d => d.UserId == _userManager.GetUserId(User))
                .ToListAsync();

            return View(discussions);
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussions = await _context.Discussions
                .Include(d => d.User) 
                .Include(d => d.Comments) 
                .FirstOrDefaultAsync(m => m.DiscussionsId == id);

            var currentUserId = _userManager.GetUserId(User);

            if (discussions.UserId != currentUserId)
            {
                return Forbid(); 
            }

            
            if (discussions == null)
            {
                return NotFound();
            }

            return View(discussions);
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {

            var discussions = new Discussions
            {
                UserId = _userManager.GetUserId(User) ?? throw new InvalidOperationException("User ID cannot be null")
            };

            return View();
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionsId,Title,Content,ImageFile")] Discussions discussions)
        {

            //Ensure the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            discussions.UserId = userId;

            discussions.CreateDate = DateTime.Now;

            if (discussions.ImageFile != null)
            {
                discussions.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussions.ImageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", discussions.ImageFilename);

                try
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussions.ImageFile.CopyToAsync(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "File upload failed: " + ex.Message);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(discussions);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("DiscussionsDetails", "Home", new { id = discussions.DiscussionsId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Database save failed: " + ex.Message);
                }
            }
            else
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                    }
                }
            }

            return View(discussions);
        }


        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussions = await _context.Discussions.FindAsync(id);
            if (discussions == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);

            if (discussions.UserId != currentUserId)
            {
                return Forbid(); 
            }

            return View(discussions);
        }

        // POST: Discussions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionsId,Title,Content")] Discussions discussions)
        {
            if (id != discussions.DiscussionsId)
            {
                return NotFound();
            }

            var existingDiscussion = await _context.Discussions.FindAsync(id);
            if (existingDiscussion == null)
            {
                return NotFound();
            }

            var currentUserId = _userManager.GetUserId(User);
            if (existingDiscussion.UserId != currentUserId)
            {
                return Unauthorized(); 
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionsExists(discussions.DiscussionsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discussions);
        }

        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussions = await _context.Discussions
                .FirstOrDefaultAsync(m => m.DiscussionsId == id);

            var currentUserId = _userManager.GetUserId(User);

            if (discussions == null)
            {
                return NotFound();
            }

            if (discussions.UserId != currentUserId)
            {
                return Unauthorized(); 
            }

            return View(discussions);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussions = await _context.Discussions.FindAsync(id);
            if (discussions != null)
            {

                var currentUserId = _userManager.GetUserId(User);

                if (discussions == null)
                {
                    return NotFound();
                }

                if (discussions.UserId != currentUserId)
                {
                    return Unauthorized(); 
                }

                _context.Discussions.Remove(discussions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionsExists(int id)
        {
            return _context.Discussions.Any(e => e.DiscussionsId == id);
        }
    }
}
