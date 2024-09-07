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
    public class PassTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PassTables
        public async Task<IActionResult> Index()
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Manager" && (role != "Admin")) // Проверяем роль пользователя
            {
                TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                return RedirectToAction("Index", "Home");
            }
            var dataVisitorsTable = await _context.PassTable
                .Include(x=>x.VisitorsPass)
                .ToListAsync();
              return _context.PassTable != null ? 
                          View(await _context.PassTable.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PassTable'  is null.");
        }

        // GET: PassTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PassTable == null)
            {
                return NotFound();
            }

            var passTable = await _context.PassTable
                .FirstOrDefaultAsync(m => m.Id_pass == id);
            if (passTable == null)
            {
                return NotFound();
            }

            return View(passTable);
        }

        // GET: PassTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PassTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_pass,VisitorPassId,Pass_type,NumEntry,TimeLeft,Price")] PassTable passTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passTable);
        }

        // GET: PassTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PassTable == null)
            {
                return NotFound();
            }

            var passTable = await _context.PassTable.FindAsync(id);
            if (passTable == null)
            {
                return NotFound();
            }
            return View(passTable);
        }

        // POST: PassTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_pass,VisitorPassId,Pass_type,NumEntry,TimeLeft,Price")] PassTable passTable)
        {
            if (id != passTable.Id_pass)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassTableExists(passTable.Id_pass))
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
            return View(passTable);
        }

        // GET: PassTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PassTable == null)
            {
                return NotFound();
            }

            var passTable = await _context.PassTable
                .FirstOrDefaultAsync(m => m.Id_pass == id);
            if (passTable == null)
            {
                return NotFound();
            }

            return View(passTable);
        }

        // POST: PassTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PassTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PassTable'  is null.");
            }
            var passTable = await _context.PassTable.FindAsync(id);
            if (passTable != null)
            {
                _context.PassTable.Remove(passTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassTableExists(int id)
        {
          return (_context.PassTable?.Any(e => e.Id_pass == id)).GetValueOrDefault();
        }
    }
}
