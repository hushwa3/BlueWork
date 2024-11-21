using BlueWork.web.BlueWorkAuth;
using BlueWork.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueWork.web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Registration model)
        {
            if (ModelState.IsValid)
            {
                UserAccount user = new UserAccount
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };
            }
            return View(model);
        }
    }
}
