using Microsoft.AspNetCore.Mvc;
using skress.Models;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using System.Data.SqlTypes;


namespace skress.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public List<avgprice> Getavgprcierent()
        {
            List<avgprice> rentprices = new List<avgprice>();
            string connectionstring = "MyDbConnection";
            
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand comand = new SqlCommand("Getavgpricerent", connection);
                comand.CommandType = CommandType.StoredProcedure;
                

                connection.Open();
                using (SqlDataReader reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rentprices.Add(new avgprice
                        {
                            Equipname = reader["Equip_Type"].ToString(),
                            Size = (float)reader["Size"],
                            rentprice = (decimal)(SqlMoney)reader["AVG_Rent_price"]
                        });
                    }
                }
            }
            return rentprices;
        }

                

        // Действие для установки роли "Менеджер" и перенаправления на страницу таблиц
        public IActionResult Manager()
        {
            HttpContext.Session.SetString("Role", "Manager"); // Установка роли "Менеджер"
            return RedirectToAction("Index"); // Перенаправляем пользователя на страницу таблиц
        }

        // Действие для установки роли "Администратор" и перенаправления на страницу таблиц
        public IActionResult Admin()
        {
            HttpContext.Session.SetString("Role", "Admin"); // Установка роли "Администратор"
            return RedirectToAction("Index"); // Перенаправляем пользователя на страницу таблиц
        }


        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
