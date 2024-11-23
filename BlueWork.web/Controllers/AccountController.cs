using BlueWork.web.Data;
using BlueWork.web.BlueWorkAuth;
using BlueWork.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace BlueWork.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlueWorkDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(BlueWorkDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Registration model)
        {
            if (ModelState.IsValid)
            {
                if (_context.UserAccounts.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "An account with this email already exists.");
                    return View(model);
                }

                var account = new UserAccount
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = HashPassword(model.Password),
                    Role = model.Role
                };

                try
                {
                    _context.UserAccounts.Add(account);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                    return RedirectToAction("Home", "Home");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during registration");
                    ModelState.AddModelError("", "An error occurred while processing your registration.");
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var account = _context.UserAccounts
                    .FirstOrDefault(a => a.Email == model.Email && VerifyPassword(model.Password, a.Password));

                if (account != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.Email),
                        new Claim(ClaimTypes.GivenName, account.FirstName),
                        new Claim(ClaimTypes.Role, account.Role)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return account.Role switch
                    {
                        "Client" => RedirectToAction("Client_Dashboard", "Home"),
                        "Worker" => RedirectToAction("JobsView", "Home"),
                        _ => RedirectToAction("Home", "Home")
                    };
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home", "Home");
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return HashPassword(enteredPassword) == storedHash;
        }
    }
}
