using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DogForum.Data;
using DogForum.Models;

namespace DogForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly DogForumContext _context;

        public CommentsController(DogForumContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var dogForumContext = _context.Comments.Include(c => c.Discussions);
            return View(await dogForumContext.ToListAsync());
        }

        // GET: Comments/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["DiscussionsId"] = id;

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentsId,Content,CreateDate,DiscussionsId")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comments);
                await _context.SaveChangesAsync();
                return RedirectToAction("DiscussionsDetails", "Home", new { id = comments.DiscussionsId });
            }
            ViewData["DiscussionsId"] = new SelectList(_context.Discussions, "DiscussionsId", "Title", comments.DiscussionsId);
            return View(comments);
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.CommentsId == id);
        }
    }
}
