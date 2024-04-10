using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Versiones01.Models;

namespace Versiones01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string version)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(version))
                {
                    ModelState.AddModelError("", "Por favor, ingrese una versi�n.");
                    return View();
                }

                var software = SoftwareManager.GetAllSoftware()
                    .Where(s => VersionComparer.Compare(s.Version, version) > 0)
                    .ToList();

                if (software.Count == 0)
                {
                    ModelState.AddModelError("", "No se encontr� software con una versi�n mayor a la ingresada.");
                }
                
                return View(software);
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
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