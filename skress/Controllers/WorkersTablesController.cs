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
    public class WorkersTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkersTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkersTables
        public async Task<IActionResult> Index()
        {
            return _context.WorkersTable != null ?
                        View(await _context.WorkersTable.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.WorkersTable'  is null.");
        }

        // GET: WorkersTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkersTable == null)
            {
                return NotFound();
            }

            var workersTable = await _context.WorkersTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workersTable == null)
            {
                return NotFound();
            }

            return View(workersTable);
        }

        // GET: WorkersTable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkersTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,Password")] WorkersTable workersTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workersTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workersTable);
        }

        // GET: WorkersTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkersTable == null)
            {
                return NotFound();
            }

            var workersTable = await _context.WorkersTable.FindAsync(id);
            if (workersTable == null)
            {
                return NotFound();
            }
            return View(workersTable);
        }

        // POST: WorkersTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,Password")] WorkersTable workersTable)
        {
            if (id != workersTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workersTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkersTableExists(workersTable.Id))
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
            return View(workersTable);
        }

        // GET: WorkersTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorkersTable == null)
            {
                return NotFound();
            }

            var workersTable = await _context.WorkersTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workersTable == null)
            {
                return NotFound();
            }

            return View(workersTable);
        }

        // POST: WorkersTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkersTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WorkersTable'  is null.");
            }
            var workersTable = await _context.WorkersTable.FindAsync(id);
            if (workersTable != null)
            {
                _context.WorkersTable.Remove(workersTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkersTableExists(int id)
        {
            return (_context.WorkersTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}