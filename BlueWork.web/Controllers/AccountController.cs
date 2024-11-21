using BlueWork.web.BlueWorkAuth;
using BlueWork.web.Entities;
using BlueWork.web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Security.Claims;

namespace BlueWork.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly EntityDbContext _context;
        public AccountController(EntityDbContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            return View(_context.UserAccounts.ToList());
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
                UserAccount account = new UserAccount();
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Email = model.Email;
                account.Password = model.Password;

                try
                {
                    _context.UserAccounts.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} Please Login.";

                }
                catch (DbUpdateException ex)
                {

                    ModelState.AddModelError("", "Please enter unique Email or Wassword");
                    return View(model);
                }
                return View();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var account = _context.UserAccounts.Where(a => a.Email == model.Email && a.Password == model.Password).FirstOrDefault();
                if (account != null)
                {
                    ModelState.Clear();
                    ViewBag.Message = "Login Successfull";
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.Email),
                        new Claim(ClaimTypes.Role, "User")
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or Password");
                    return View(model);
                }
            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home");

        }
        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identities.First().Name;
            return View();
        }
    }

}