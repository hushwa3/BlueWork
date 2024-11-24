using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlueWork.web.Data;
using BlueWork.web.Models;
using System.Security.Claims;
using Newtonsoft.Json;
using BlueWork.web.BlueWorkAuth;

namespace BlueWork.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly BlueWorkDbContext _context;

        public AccountController(BlueWorkDbContext context)
        {
            _context = context;
        }

        // GET: Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid form submission." });
            }

            var account = _context.UserAccounts.FirstOrDefault(u => u.Email == model.Email);
            if (account == null || !VerifyPassword(model.Password, account.Password))
            {
                return Json(new { success = false, message = "Invalid email or password." });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.Role, account.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return Json(new { success = true, redirectUrl = Url.Action("Home", "Home") });
        }

        // GET: Registration
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        // POST: Registration
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(Registration model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(m => m.Value.Errors.Any())
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    Console.WriteLine(JsonConvert.SerializeObject(errors));
                    return Json(new { success = false, errors });
                }

                if (_context.UserAccounts.Any(u => u.Email == model.Email))
                {
                    return Json(new { success = false, message = "Email already exists." });
                }

                // Assign a unique ID (for string-based RegisterID, adjust UserAccount)
                var account = new UserAccount
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = HashPassword(model.Password),
                    Role = model.Role
                };

                _context.UserAccounts.Add(account);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Registration successful!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return Json(new { success = false, message = "An internal error occurred.", error = ex.Message });
            }
        }

        // POST: Logout
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home", "Home");
        }

        // Password hashing method
        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // Password verification method
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return HashPassword(enteredPassword) == storedHash;
        }
    }
}
