using Microsoft.AspNetCore.Mvc;
using skress.Models;
using System.Diagnostics;

namespace skress.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
