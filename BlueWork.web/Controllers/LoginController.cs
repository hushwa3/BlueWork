using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace BlueWork.web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Home()
        {
            return View("~/Views/Home/Login.cshtml");
        }
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var authResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var claims = authResult.Principal.Identities.FirstOrDefault().Claims.Select(claim => new { 
                claim.Issuer, 
                claim.OriginalIssuer, 
                claim.Type, 
                claim.Value }).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return RedirectToAction("Home", "Home", new { area = "" });
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("~/Views/Home/Home.cshtml");
        }

    }
}
