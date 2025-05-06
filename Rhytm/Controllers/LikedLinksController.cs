using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rhytm.Models;

namespace Rhytm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikedLinksController : Controller
    {
        private readonly RhytmContext _context;

        public LikedLinksController(RhytmContext context)
        {
            _context = context;
        }

        // GET: LikedLinks
        public async Task<IActionResult> Index()
        {
            var rhytmContext = _context.LikedLinks.Include(l => l.Track).Include(l => l.User);
            return View(await rhytmContext.ToListAsync());
        }

        // GET: LikedLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likedLink = await _context.LikedLinks
                .Include(l => l.Track)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (likedLink == null)
            {
                return NotFound();
            }

            return View(likedLink);
        }

        // GET: LikedLinks/Create
        public IActionResult Create()
        {
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: LikedLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrackId,UserId")] LikedLink likedLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(likedLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", likedLink.TrackId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", likedLink.UserId);
            return View(likedLink);
        }

        // GET: LikedLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likedLink = await _context.LikedLinks.FindAsync(id);
            if (likedLink == null)
            {
                return NotFound();
            }
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", likedLink.TrackId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", likedLink.UserId);
            return View(likedLink);
        }

        // POST: LikedLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrackId,UserId")] LikedLink likedLink)
        {
            if (id != likedLink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(likedLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikedLinkExists(likedLink.Id))
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
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", likedLink.TrackId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", likedLink.UserId);
            return View(likedLink);
        }

        // GET: LikedLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likedLink = await _context.LikedLinks
                .Include(l => l.Track)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (likedLink == null)
            {
                return NotFound();
            }

            return View(likedLink);
        }

        // POST: LikedLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var likedLink = await _context.LikedLinks.FindAsync(id);
            if (likedLink != null)
            {
                _context.LikedLinks.Remove(likedLink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikedLinkExists(int id)
        {
            return _context.LikedLinks.Any(e => e.Id == id);
        }
    }
}
