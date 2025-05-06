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
    public class AuthorshipsController : Controller
    {
        private readonly RhytmContext _context;

        public AuthorshipsController(RhytmContext context)
        {
            _context = context;
        }

        // GET: Authorships
        public async Task<IActionResult> Index()
        {
            var rhytmContext = _context.Authorships.Include(a => a.Artist).Include(a => a.Track);
            return View(await rhytmContext.ToListAsync());
        }

        // GET: Authorships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorships
                .Include(a => a.Artist)
                .Include(a => a.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorship == null)
            {
                return NotFound();
            }

            return View(authorship);
        }

        // GET: Authorships/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id");
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id");
            return View();
        }

        // POST: Authorships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrackId,ArtistId")] Authorship authorship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", authorship.ArtistId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", authorship.TrackId);
            return View(authorship);
        }

        // GET: Authorships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorships.FindAsync(id);
            if (authorship == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", authorship.ArtistId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", authorship.TrackId);
            return View(authorship);
        }

        // POST: Authorships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrackId,ArtistId")] Authorship authorship)
        {
            if (id != authorship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorshipExists(authorship.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Id", authorship.ArtistId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", authorship.TrackId);
            return View(authorship);
        }

        // GET: Authorships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorship = await _context.Authorships
                .Include(a => a.Artist)
                .Include(a => a.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorship == null)
            {
                return NotFound();
            }

            return View(authorship);
        }

        // POST: Authorships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var authorship = await _context.Authorships.FindAsync(id);
            if (authorship != null)
            {
                _context.Authorships.Remove(authorship);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorshipExists(int id)
        {
            return _context.Authorships.Any(e => e.Id == id);
        }
    }
}
