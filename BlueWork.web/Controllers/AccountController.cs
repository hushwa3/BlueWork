using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlueWork.web.Data;
using BlueWork.web.Models;
using System.Security.Claims;
using System.Linq;
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
            if (ModelState.IsValid)
            {
                var account = _context.UserAccounts
                    .FirstOrDefault(u => u.Email == model.Email && VerifyPassword(model.Password, u.Password));

                if (account != null)
                {
                    // Create claims for authenticated user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.Email),
                        new Claim(ClaimTypes.GivenName, account.FirstName),
                        new Claim(ClaimTypes.Role, account.Role)
                    };

                    // Create identity and sign in
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return Json(new { success = true, redirectUrl = Url.Action("Home", "Home") });
                }

                return Json(new { success = false, message = "Invalid email or password." });
            }

            // Return validation errors
            var errors = ModelState
                .Where(m => m.Value.Errors.Any())
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return Json(new { success = false, errors });
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
        public IActionResult Registration(Registration model)
        {
            try
            {
                // Log the incoming data for debugging
                Console.WriteLine($"FirstName: {model.FirstName}, LastName: {model.LastName}, Email: {model.Email}, Role: {model.Role}");

                if (ModelState.IsValid)
                {
                    if (_context.UserAccounts.Any(u => u.Email == model.Email))
                    {
                        ModelState.AddModelError("Email", "An account with this email already exists.");
                    }
                    else
                    {
                        var account = new UserAccount
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            Password = HashPassword(model.Password),
                            Role = model.Role // Ensure Role is populated
                        };

                        _context.UserAccounts.Add(account);
                        _context.SaveChanges();

                        return Json(new { success = true, message = "Registration successful!" });
                    }
                }

                // Capture validation errors
                var errors = ModelState
                    .Where(m => m.Value.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { success = false, errors });
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error during registration: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return Json(new { success = false, message = "An internal error occurred." });
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
