using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlueWork.web.Data;
using Microsoft.AspNetCore.Identity;
using BlueWork.web.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlueWork.web.Controllers
{
    [Authorize(Roles = "Admin")]
   

    public class UserAccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserAccountsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                // Create the user with the provided password
                var user = new ApplicationUser
                {
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    Email = applicationUser.Email,
                    UserName = applicationUser.Email
                };

                var result = await _userManager.CreateAsync(user, applicationUser.PasswordHash);

                

                // Add errors to the ModelState if creation fails
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(applicationUser);
        }
    }

}
