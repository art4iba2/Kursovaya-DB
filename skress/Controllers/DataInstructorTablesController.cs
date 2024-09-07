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
    public class DataInstructorTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataInstructorTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DataInstructorTables
        public async Task<IActionResult> Index()
        {

            string role = HttpContext.Session.GetString("Role");
            if (role != "Manager" && (role != "Admin")) // Проверяем роль пользователя
            {
                TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                return RedirectToAction("Index", "Home");
            }
            
            var  dataInstructorsTable = await _context.dataInstructorTable
                .Include(d => d.Instructor)
                .ToListAsync();
            var dataVisitorsTable = await _context.dataInstructorTable
                .Include(d => d.Visitors)
                .ToListAsync();
             
            return _context.dataInstructorTable != null ?
                          View(await _context.dataInstructorTable.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Lot' is null.");

            
        }

        // GET: DataInstructorTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.dataInstructorTable == null)
            {
                return NotFound();
            }

            var dataInstructorTable = await _context.dataInstructorTable
                .FirstOrDefaultAsync(m => m.Id_Event == id);
            if (dataInstructorTable == null)
            {
                return NotFound();
            }

            return View(dataInstructorTable);
        }

        // GET: DataInstructorTables/Create
        public IActionResult Create()
        {
            ViewBag.InstructorList = new SelectList(_context.InstructorsTable, "Id_instructors", "FullName");
            return View();
        }

        // POST: DataInstructorTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Event,Id_visitor,Id_Instructor,Price,TimeStart,TimeEnd")] DataInstructorTable dataInstructorTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataInstructorTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataInstructorTable);
        }

        // GET: DataInstructorTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.InstructorList = new SelectList(_context.InstructorsTable, "Id_instructors", "FullName");
            if (id == null || _context.dataInstructorTable == null)
            {
                return NotFound();
            }

            var dataInstructorTable = await _context.dataInstructorTable.FindAsync(id);
            if (dataInstructorTable == null)
            {
                return NotFound();
            }
            return View(dataInstructorTable);
        }

        // POST: DataInstructorTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Event,Id_visitor,Id_Instructor,Price,TimeStart,TimeEnd")] DataInstructorTable dataInstructorTable)
        {
            if (id != dataInstructorTable.Id_Event)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataInstructorTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataInstructorTableExists(dataInstructorTable.Id_Event))
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
            return View(dataInstructorTable);
        }

        // GET: DataInstructorTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.dataInstructorTable == null)
            {
                return NotFound();
            }

            var dataInstructorTable = await _context.dataInstructorTable
                .FirstOrDefaultAsync(m => m.Id_Event == id);
            if (dataInstructorTable == null)
            {
                return NotFound();
            }

            return View(dataInstructorTable);
        }

        // POST: DataInstructorTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.dataInstructorTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.dataInstructorTable'  is null.");
            }
            var dataInstructorTable = await _context.dataInstructorTable.FindAsync(id);
            if (dataInstructorTable != null)
            {
                _context.dataInstructorTable.Remove(dataInstructorTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataInstructorTableExists(int id)
        {
          return (_context.dataInstructorTable?.Any(e => e.Id_Event == id)).GetValueOrDefault();
        }
    }
}
