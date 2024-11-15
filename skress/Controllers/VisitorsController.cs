using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using skress.Models;
using System.Data;
using System.Data.SqlClient;

namespace skress.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly string _connectionString = "Server=LAPTOP-P6JDQUOH\\SQLEXPRESS;Database=Ski_resort;Trusted_Connection=True;";

        public async Task<IActionResult> Index()
        {
            List<VisitorInfo> visitorData = new List<VisitorInfo>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetUniqueVisitors", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            visitorData.Add(new VisitorInfo
                            {
                                Id_visitor = reader.GetInt32(0),
                                VisitorName = reader.GetString(1),
                                VisitorSurname = reader.GetString(2),
                                VisitorBirthday = reader.GetDateTime(3),
                                PassType = reader.IsDBNull(4) ? null : reader.GetString(4),
                                NumberOfEntries = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                                EquipmentType = reader.IsDBNull(6) ? null : reader.GetString(6),
                                TrackName = reader.IsDBNull(7) ? null : reader.GetString(7),
                                TrackDifficulty = reader.IsDBNull(8) ? (int?)null : reader.GetInt16(8),
                                InstructorName = reader.IsDBNull(9) ? null : reader.GetString(9),
                                InstructorSurname = reader.IsDBNull(10) ? null : reader.GetString(10),
                                InstructorSpecialization = reader.IsDBNull(11) ? null : reader.GetString(11)
                            });
                        }
                    }
                }
            }

            return View(visitorData);

        }
    }
}
