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
    public class DiscussionsController : Controller
    {
        private readonly DogForumContext _context;

        public DiscussionsController(DogForumContext context)
        {
            _context = context;
        }

        // GET: Discussions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Discussions.ToListAsync());
        }

        // GET: Discussions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussions = await _context.Discussions
                .FirstOrDefaultAsync(m => m.DiscussionsId == id);
            if (discussions == null)
            {
                return NotFound();
            }

            return View(discussions);
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionsId,Title,Content")] Discussions discussions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discussions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
            if (discussions == null)
            {
                return NotFound();
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
