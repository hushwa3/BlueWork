using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using System.Dynamic;
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
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return RedirectToAction("Home", "Home", new { area = ""});
    }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("~/Views/Home/Home.cshtml");
        }

    }
}
