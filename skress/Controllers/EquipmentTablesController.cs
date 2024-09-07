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
    public class EquipmentTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipmentTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentTables
        public async Task<IActionResult> Index()
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Manager" && (role != "Admin")) // Проверяем роль пользователя
            {
                TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                return RedirectToAction("Index", "Home");
            }
            return _context.EquipmentTable != null ? 
                          View(await _context.EquipmentTable.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.EquipmentTable'  is null.");
        }

        // GET: EquipmentTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EquipmentTable == null)
            {
                return NotFound();
            }

            var equipmentTable = await _context.EquipmentTable
                .FirstOrDefaultAsync(m => m.Id_equipment == id);
            if (equipmentTable == null)
            {
                return NotFound();
            }

            return View(equipmentTable);
        }

        // GET: EquipmentTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EquipmentTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_equipment,EquipmentType,Size")] EquipmentTable equipmentTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipmentTable);
        }

        // GET: EquipmentTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EquipmentTable == null)
            {
                return NotFound();
            }

            var equipmentTable = await _context.EquipmentTable.FindAsync(id);
            if (equipmentTable == null)
            {
                return NotFound();
            }
            return View(equipmentTable);
        }

        // POST: EquipmentTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_equipment,EquipmentType,Size")] EquipmentTable equipmentTable)
        {
            if (id != equipmentTable.Id_equipment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentTableExists(equipmentTable.Id_equipment))
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
            return View(equipmentTable);
        }

        // GET: EquipmentTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EquipmentTable == null)
            {
                return NotFound();
            }

            var equipmentTable = await _context.EquipmentTable
                .FirstOrDefaultAsync(m => m.Id_equipment == id);
            if (equipmentTable == null)
            {
                return NotFound();
            }

            return View(equipmentTable);
        }

        // POST: EquipmentTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EquipmentTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EquipmentTable'  is null.");
            }
            var equipmentTable = await _context.EquipmentTable.FindAsync(id);
            if (equipmentTable != null)
            {
                _context.EquipmentTable.Remove(equipmentTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentTableExists(int id)
        {
          return (_context.EquipmentTable?.Any(e => e.Id_equipment == id)).GetValueOrDefault();
        }
    }
}
