using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using skress.Models;

namespace skress.Controllers
{
    [AuthenticatedUser]
    public class TrackTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrackTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrackTables
        public async Task<IActionResult> Index()
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Admin") // Проверяем роль пользователя
            {
                TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                return RedirectToAction("Index", "Home");
            }
            return _context.TrackTable != null ? 
                          View(await _context.TrackTable.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TrackTable'  is null.");
        }

        // GET: TrackTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrackTable == null)
            {
                return NotFound();
            }

            var trackTable = await _context.TrackTable
                .FirstOrDefaultAsync(m => m.Id_track == id);
            if (trackTable == null)
            {
                return NotFound();
            }

            return View(trackTable);
        }

        // GET: TrackTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrackTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_track,NameTrack,Difficulty,TrackLength,TrackDelta,TrackWidth")] TrackTable trackTable)
        {
            if (ModelState.IsValid)
            {
                string role = HttpContext.Session.GetString("Role");
                if (role != "Admin") // Проверяем роль пользователя
                {
                    TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                    return RedirectToAction("Index", "Home");
                }
                _context.Add(trackTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trackTable);
        }

        // GET: TrackTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrackTable == null)
            {
                return NotFound();
            }

            var trackTable = await _context.TrackTable.FindAsync(id);
            if (trackTable == null)
            {
                return NotFound();
            }
            return View(trackTable);
        }

        // POST: TrackTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_track,NameTrack,Difficulty,TrackLength,TrackDelta,TrackWidth")] TrackTable trackTable)
        {
            if (id != trackTable.Id_track)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trackTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackTableExists(trackTable.Id_track))
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
            return View(trackTable);
        }

        // GET: TrackTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrackTable == null)
            {
                return NotFound();
            }

            var trackTable = await _context.TrackTable
                .FirstOrDefaultAsync(m => m.Id_track == id);
            if (trackTable == null)
            {
                return NotFound();
            }

            return View(trackTable);
        }

        // POST: TrackTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrackTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TrackTable'  is null.");
            }
            var trackTable = await _context.TrackTable.FindAsync(id);
            if (trackTable != null)
            {
                _context.TrackTable.Remove(trackTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackTableExists(int id)
        {
          return (_context.TrackTable?.Any(e => e.Id_track == id)).GetValueOrDefault();
        }
    }
}
