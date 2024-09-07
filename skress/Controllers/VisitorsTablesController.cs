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
    public class VisitorsTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitorsTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VisitorsTables
        public async Task<IActionResult> Index(string searchString)
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Manager" && (role != "Admin")) // Проверяем роль пользователя
            {
                TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                return RedirectToAction("Index", "Home");
            }
            //создается запрос LINQ для выбора посетителя
            var visitor = from m in _context.VisitorsTable
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                visitor = visitor.Where(s => s.Name!.Contains(searchString));
            }

            return View(await visitor.ToListAsync());
            return _context.VisitorsTable != null ? 
                          View(await _context.VisitorsTable.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.VisitorsTable'  is null.");
        }

        

        // GET: VisitorsTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VisitorsTable == null)
            {
                return NotFound();
            }

            var visitorsTable = await _context.VisitorsTable
                .FirstOrDefaultAsync(m => m.Id_visitor == id);
            if (visitorsTable == null)
            {
                return NotFound();
            }

            return View(visitorsTable);
        }

        // GET: VisitorsTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VisitorsTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_visitor,Name,Surname,BirthdayDate,Sex")] VisitorsTable visitorsTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visitorsTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visitorsTable);
        }

        // GET: VisitorsTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VisitorsTable == null)
            {
                return NotFound();
            }

            var visitorsTable = await _context.VisitorsTable.FindAsync(id);
            if (visitorsTable == null)
            {
                return NotFound();
            }
            return View(visitorsTable);
        }

        // POST: VisitorsTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_visitor,Name,Surname,BirthdayDate,Sex")] VisitorsTable visitorsTable)
        {
            if (id != visitorsTable.Id_visitor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitorsTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorsTableExists(visitorsTable.Id_visitor))
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
            return View(visitorsTable);
        }

        // GET: VisitorsTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VisitorsTable == null)
            {
                return NotFound();
            }

            var visitorsTable = await _context.VisitorsTable
                .FirstOrDefaultAsync(m => m.Id_visitor == id);
            if (visitorsTable == null)
            {
                return NotFound();
            }

            return View(visitorsTable);
        }

        // POST: VisitorsTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VisitorsTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VisitorsTable'  is null.");
            }
            var visitorsTable = await _context.VisitorsTable.FindAsync(id);
            if (visitorsTable != null)
            {
                _context.VisitorsTable.Remove(visitorsTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorsTableExists(int id)
        {
          return (_context.VisitorsTable?.Any(e => e.Id_visitor == id)).GetValueOrDefault();
        }
    }
}
