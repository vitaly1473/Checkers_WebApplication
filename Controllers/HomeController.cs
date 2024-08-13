using Checkers_WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Checkers_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       
        // ����� Index ������������ ������� � ��������� URL (/Home/Index ��� ������ /, ���� �������� �� ���������)
        public IActionResult Index()
        {
            return View(); //���������� ������������� Index, ������������� � ����� Views/ Home
        }
        public IActionResult About()
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
