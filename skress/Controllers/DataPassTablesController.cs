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
    public class DataPassTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataPassTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DataPassTables
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Manager" && (role != "Admin")) // Проверяем роль пользователя
            {
                TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                return RedirectToAction("Index", "Home");
            }

            // Запрос данных с учетом фильтрации по диапазону дат
            var query = _context.DataPassTable.Include(x => x.Track).Include(x => x.Pass).AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(x => x.DatePass >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(x => x.DatePass <= endDate.Value);
            }

            var dataTable = await query.ToListAsync();
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
                        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

                        var dataTrackTable = await _context.DataPassTable
                            .Include(x=>x.Track)
                            .ToListAsync();
                        var dataPassTable = await _context.DataPassTable
                          .Include(x => x.Pass)
                          .ToListAsync();
            return View(dataTable);

            // Передача диапазона дат обратно в View через ViewBag
            
            return _context.DataPassTable != null ? 
                          View(await _context.DataPassTable.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DataPassTable'  is null.");
        }

        // GET: DataPassTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DataPassTable == null)
            {
                return NotFound();
            }

            var dataPassTable = await _context.DataPassTable
                .FirstOrDefaultAsync(m => m.Id_datapass == id);
            if (dataPassTable == null)
            {
                return NotFound();
            }

            return View(dataPassTable);
        }

        // GET: DataPassTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataPassTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_datapass,Id_Track,Id_Pass,DatePass")] DataPassTable dataPassTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataPassTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataPassTable);
        }

        // GET: DataPassTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DataPassTable == null)
            {
                return NotFound();
            }

            var dataPassTable = await _context.DataPassTable.FindAsync(id);
            if (dataPassTable == null)
            {
                return NotFound();
            }
            return View(dataPassTable);
        }

        // POST: DataPassTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_datapass,Id_Track,Id_Pass,DatePass")] DataPassTable dataPassTable)
        {
            if (id != dataPassTable.Id_datapass)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataPassTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataPassTableExists(dataPassTable.Id_datapass))
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
            return View(dataPassTable);
        }

        // GET: DataPassTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DataPassTable == null)
            {
                return NotFound();
            }

            var dataPassTable = await _context.DataPassTable
                .FirstOrDefaultAsync(m => m.Id_datapass == id);
            if (dataPassTable == null)
            {
                return NotFound();
            }

            return View(dataPassTable);
        }

        // POST: DataPassTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DataPassTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DataPassTable'  is null.");
            }
            var dataPassTable = await _context.DataPassTable.FindAsync(id);
            if (dataPassTable != null)
            {
                _context.DataPassTable.Remove(dataPassTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataPassTableExists(int id)
        {
          return (_context.DataPassTable?.Any(e => e.Id_datapass == id)).GetValueOrDefault();
        }
    }
}
