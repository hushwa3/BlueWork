using BlueWork.web.Data;
using BlueWork.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlueWork.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlueWorkDbContext _context;

        // Injecting dependencies
        public HomeController(
            BlueWorkDbContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Join()
        {
            return View();
        }

        public IActionResult Client_Profile()
        {
            return View();
        }

        public IActionResult WorkerProfile_Setup()
        {
            return View();
        }
        public IActionResult WorkerProfile()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> ApplyJob(int id)
        {
            // Fetch the job post with the related user data
            var jobPost = await _context.JobPosts
                .FirstOrDefaultAsync(j => j.Id == id);

            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }


        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Client_DashboardAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var jobPosts = await _context.JobPosts
            .Where(j => j.UserId == userId)
            .ToListAsync();
            return View(jobPosts);
        }

        [HttpGet]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> JobsViewAsync()
        {
            var jobPosts = await _context.JobPosts.ToListAsync();
            return View(jobPosts); 
        }
        [HttpGet]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> SearchJobsAsync()
        {
            var jobPosts = await _context.JobPosts.ToListAsync();
            return View(jobPosts);
        }

        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> JobPost(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // Fetch the specific job post by Id and ensure it belongs to the logged-in user
            var jobPost = await _context.JobPosts
                .FirstOrDefaultAsync(j => j.Id == id && j.UserId == userId);
            if (jobPost == null)
            {
                // Return a Not Found view if the job does not exist or does not belong to the user
                return NotFound();
            }
            return View(jobPost); 
        }


        public IActionResult ReviewProposal()
        {
            return View();
        }

        public IActionResult Hire()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(AddPostModel.InputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var jobPost = new JobPost
            {
                Headline = model.Headline,
                Skills = string.Join(",", model.Skills),
                Complexity = model.Complexity,
                Duration = model.Duration,
                Experience = model.Experience,
                HourlyRateFrom = model.HourlyRateFrom,
                HourlyRateTo = model.HourlyRateTo,
                ProjectBudget = model.ProjectBudget,
                Description = model.Description
            };
            jobPost.UserId = userId;
            _context.JobPosts.Add(jobPost);
            _context.SaveChanges();

            return RedirectToAction("JobPostSuccess");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
