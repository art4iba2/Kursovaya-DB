using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using skress.Models;
using System.Data.SqlClient;

namespace skress.Controllers
{
    [AuthenticatedUser]
    public class DataRentTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataRentTablesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<avgprice> MyProcedure()
        {
            List<avgprice> rentprices = new List<avgprice>();
            string connectionstring = "Server=LAPTOP-P6JDQUOH\\SQLEXPRESS;Database=Ski_resort;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand comand = new SqlCommand("MyProcedure", connection);
                comand.CommandType = CommandType.StoredProcedure;


                connection.Open();
                using (SqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentprices.Add(new avgprice
                        {
                            Equipname = reader["Equip_Type"].ToString(),
                            Size = Convert.ToSingle(reader["Size"]),
                            rentprice = (decimal)reader["AVG_Rent_price"]
                        });
                    }
                }
            }
            return rentprices;
        }
        public ActionResult ShowAvgPrices()
        {
            List<avgprice> rentPrices = MyProcedure();
            return View(rentPrices);
        }
        // GET: DataRentTables
        public async Task<IActionResult> Index()
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Manager" && (role != "Admin")) // Проверяем роль пользователя
            {
                TempData["ErrorMessage"] = "У вас нет доступа к этой функции";
                return RedirectToAction("Index", "Home");
            }
            var dataVisitorRent = await _context.DataRentTable
                .Include(x=>x.VisitorEq)
                .ToListAsync();
            var dataEquipmentRent = await _context.DataRentTable
                .Include(x=>x.Equip)
                .ToListAsync();
              return _context.DataRentTable != null ? 
                          View(await _context.DataRentTable.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DataRentTable'  is null.");
        }

        // GET: DataRentTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DataRentTable == null)
            {
                return NotFound();
            }

            var dataRentTable = await _context.DataRentTable
                .FirstOrDefaultAsync(m => m.Id_DataRent == id);
            if (dataRentTable == null)
            {
                return NotFound();
            }

            return View(dataRentTable);
        }

        // GET: DataRentTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataRentTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_DataRent,ID_Visitor,Id_equip,VisitorsDocument,TimeStart,TimeEnd,RentPrice")] DataRentTable dataRentTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataRentTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataRentTable);
        }




        // GET: DataRentTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DataRentTable == null)
            {
                return NotFound();
            }

            var dataRentTable = await _context.DataRentTable.FindAsync(id);
            if (dataRentTable == null)
            {
                return NotFound();
            }
            return View(dataRentTable);
        }

        // POST: DataRentTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_DataRent,ID_Visitor,Id_equip,VisitorsDocument,TimeStart,TimeEnd,RentPrice")] DataRentTable dataRentTable)
        {
            if (id != dataRentTable.Id_DataRent)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataRentTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataRentTableExists(dataRentTable.Id_DataRent))
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
            return View(dataRentTable);
        }

        // GET: DataRentTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DataRentTable == null)
            {
                return NotFound();
            }

            var dataRentTable = await _context.DataRentTable
                .FirstOrDefaultAsync(m => m.Id_DataRent == id);
            if (dataRentTable == null)
            {
                return NotFound();
            }

            return View(dataRentTable);
        }

        // POST: DataRentTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DataRentTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DataRentTable'  is null.");
            }
            var dataRentTable = await _context.DataRentTable.FindAsync(id);
            if (dataRentTable != null)
            {
                _context.DataRentTable.Remove(dataRentTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataRentTableExists(int id)
        {
          return (_context.DataRentTable?.Any(e => e.Id_DataRent == id)).GetValueOrDefault();
        }
    }
}
