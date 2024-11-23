using BlueWork.web.BlueWorkAuth;
using BlueWork.web.Entities;
using BlueWork.web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace BlueWork.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly EntityDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(EntityDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_context.UserAccounts.ToList());
        }

        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View("~/Views/Account/Registration.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(Registration model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (_context.UserAccounts.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "An account with this email already exists.");
                    return View("~/Views/Account/Login.cshtml", model);
                }

                var account = new UserAccount
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = HashPassword(model.Password),
                    Role = model.Role // Assign role from the registration form
                };

                try
                {
                    _context.UserAccounts.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    TempData["Message"] = "Registration successful! Please log in.";
                    return RedirectToAction("~/Views/Account/Login.cshtml");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during registration");
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _logger.LogError("Model validation failed: {Errors}", string.Join(", ", errors));
            }
            return View("~/Views/Account/Registration.cshtml", model);
        }

        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var account = _context.UserAccounts
                    .FirstOrDefault(a => a.Email == model.Email && VerifyPassword(model.Password, a.Password));

                if (account != null)
                {
                    // Set up claims including the user's role
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.Email),
                        new Claim(ClaimTypes.GivenName, account.FirstName),
                        new Claim(ClaimTypes.Role, account.Role) // Add user's role to claims
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    // Redirect based on role
                    if (account.Role == "Client")
                        return RedirectToAction("Client_Dashboard");
                    else if (account.Role == "Worker")
                        return RedirectToAction("JobsView");

                    return RedirectToAction("~/Views/Home/Home.cshtml"); // Default redirect
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            return View("~/Views/Home/Home.cshtml", model);
        }

        [Authorize(Roles = "Client")]
        public IActionResult Client_Dashboard()
        {
            return View("~/Views/Home/Client_Dashboard.cshtml");
        }

        [Authorize(Roles = "Worker")]
        public IActionResult JobsView()
        {
            return View("~/Views/Home/JobsView.cshtml");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("~/Views/Home/Home.cshtml");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            return View("~/Views/Account/Login.cshtml");
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
