using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NetCoreOptionSample.Models;

namespace NetCoreOptionSample.Controllers
{
    public class HomeController : Controller
    {
        private AuthConfig _authConfig;

        public HomeController(AuthConfig authConfig)
        {
            _authConfig = authConfig;
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
