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
    public class PlaylistLinksController : Controller
    {
        private readonly RhytmContext _context;

        public PlaylistLinksController(RhytmContext context)
        {
            _context = context;
        }

        // GET: PlaylistLinks
        public async Task<IActionResult> Index()
        {
            var rhytmContext = _context.PlaylistLinks.Include(p => p.Playlist).Include(p => p.Track);
            return View(await rhytmContext.ToListAsync());
        }

        // GET: PlaylistLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistLink = await _context.PlaylistLinks
                .Include(p => p.Playlist)
                .Include(p => p.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlistLink == null)
            {
                return NotFound();
            }

            return View(playlistLink);
        }

        // GET: PlaylistLinks/Create
        public IActionResult Create()
        {
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id");
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id");
            return View();
        }

        // POST: PlaylistLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrackId,PlaylistId")] PlaylistLink playlistLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playlistLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", playlistLink.PlaylistId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", playlistLink.TrackId);
            return View(playlistLink);
        }

        // GET: PlaylistLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistLink = await _context.PlaylistLinks.FindAsync(id);
            if (playlistLink == null)
            {
                return NotFound();
            }
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", playlistLink.PlaylistId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", playlistLink.TrackId);
            return View(playlistLink);
        }

        // POST: PlaylistLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrackId,PlaylistId")] PlaylistLink playlistLink)
        {
            if (id != playlistLink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlistLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistLinkExists(playlistLink.Id))
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
            ViewData["PlaylistId"] = new SelectList(_context.Playlists, "Id", "Id", playlistLink.PlaylistId);
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", playlistLink.TrackId);
            return View(playlistLink);
        }

        // GET: PlaylistLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlistLink = await _context.PlaylistLinks
                .Include(p => p.Playlist)
                .Include(p => p.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlistLink == null)
            {
                return NotFound();
            }

            return View(playlistLink);
        }

        // POST: PlaylistLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playlistLink = await _context.PlaylistLinks.FindAsync(id);
            if (playlistLink != null)
            {
                _context.PlaylistLinks.Remove(playlistLink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistLinkExists(int id)
        {
            return _context.PlaylistLinks.Any(e => e.Id == id);
        }
    }
}
