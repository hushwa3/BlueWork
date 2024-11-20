using BlueWork.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlueWork.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Join()
        {
            return View();
        }

        public IActionResult WorkerProfile() 
        {
            return View();
        }
            
            
        public IActionResult Client_Profile()
        {
            return View();
        }

        public IActionResult ClientInfoProfile()
        {
            return View();
        }
        public IActionResult WorkerProfile_Setup()
        {
            return View();
        }
        public IActionResult Client_Dashboard()
        {
            return View();
        }

        public IActionResult JobsView()
        {
            return View();
        }
        public IActionResult JobPost()
        {
            return View();
        }
        public IActionResult ReviewProposal()
        {
            return View();
        }
        public IActionResult Hire()
        {
            return View();
        }
        public IActionResult AddPost()
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
