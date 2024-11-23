using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlueWork.web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Home()
        {
            return View("~/Views/Home/Home.cshtml");
        }
        public IActionResult ClientInfoProfile()
        {
            return View("~/Views/Home/ClientInfoProfile.cshtml");
        }
        public IActionResult WorkerProfile()
        {
            return View("~/Views/Home/WorkerProfile.cshtml");
        }
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") });
        }

       public async Task<IActionResult> GoogleResponse()
{
        var authResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
    
        // Extract claims from Google authentication
        var claims = authResult.Principal.Identities.FirstOrDefault()?.Claims.ToList();
        if (claims == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect to login if no claims
        }

        // Create a claims identity for the application
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Sign in user and create authentication cookie
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return RedirectToAction("Home", "Home");
    }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("~/Views/Home/Home.cshtml");
        }

    }
}
