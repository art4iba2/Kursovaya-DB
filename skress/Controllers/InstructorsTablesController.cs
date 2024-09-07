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
    public class InstructorsTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorsTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InstructorsTables
        public async Task<IActionResult> Index()
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Admin") // Проверяем роль пользователя
            {
                TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                return RedirectToAction("Index", "Home");
            }
            return _context.InstructorsTable != null ? 
                          View(await _context.InstructorsTable.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.InstructorsTable'  is null.");
        }

        

        // GET: InstructorsTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InstructorsTable == null)
            {
                return NotFound();
            }

            var instructorsTable = await _context.InstructorsTable
                .FirstOrDefaultAsync(m => m.Id_instructors == id);
            if (instructorsTable == null)
            {
                return NotFound();
            }

            return View(instructorsTable);
        }

        // GET: InstructorsTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InstructorsTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_instructors,Name,Surname,Specialization,Experience,Rating,BirthdayDate")] InstructorsTable instructorsTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructorsTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructorsTable);
        }

        // GET: InstructorsTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InstructorsTable == null)
            {
                return NotFound();
            }

            var instructorsTable = await _context.InstructorsTable.FindAsync(id);
            if (instructorsTable == null)
            {
                return NotFound();
            }
            return View(instructorsTable);
        }

        // POST: InstructorsTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_instructors,Name,Surname,Specialization,Experience,Rating,BirthdayDate")] InstructorsTable instructorsTable)
        {
            if (id != instructorsTable.Id_instructors)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructorsTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorsTableExists(instructorsTable.Id_instructors))
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
            return View(instructorsTable);
        }

        // GET: InstructorsTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InstructorsTable == null)
            {
                return NotFound();
            }

            var instructorsTable = await _context.InstructorsTable
                .FirstOrDefaultAsync(m => m.Id_instructors == id);
            if (instructorsTable == null)
            {
                return NotFound();
            }

            return View(instructorsTable);
        }

        // POST: InstructorsTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InstructorsTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InstructorsTable'  is null.");
            }
            var instructorsTable = await _context.InstructorsTable.FindAsync(id);
            if (instructorsTable != null)
            {
                _context.InstructorsTable.Remove(instructorsTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorsTableExists(int id)
        {
          return (_context.InstructorsTable?.Any(e => e.Id_instructors == id)).GetValueOrDefault();
        }
    }
}
