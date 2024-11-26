using BlueWork.web.Data;
using BlueWork.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlueWork.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BlueWorkDbContext _context;

        // Injecting dependencies
        public HomeController(
            UserManager<ApplicationUser> userManager,
            BlueWorkDbContext context)
        {
            _userManager = userManager;
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

        [Authorize(Roles = "Client")]
        public IActionResult Client_Dashboard()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            var jobPosts = GetAddPostModels();
            return View(jobPosts); // Passes the job posts to the view
        }

        private List<JobPost> GetAddPostModels()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return [];
            }

            // Retrieve all job posts created by the logged-in user
            return _context.JobPosts
                .Where(job => job.UserId == userId)
                .ToList();
        }

        public IActionResult JobsView()
        {
            return View();
        }

        public IActionResult JobPost()
        {
            return View();
        }

        public IActionResult ReviewProposal()
        {
            return View();
        }

        public IActionResult Hire()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            var model = new AddPostModel.InputModel(); // Initialize the model
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostModel.InputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Unauthorized();
            }

            var jobPost = new JobPost
            {
                Headline = model.Headline,
                Skills = string.Join(",", model.Skills), // Convert skills list to a comma-separated string
                Complexity = model.Complexity,
                Duration = model.Duration,
                Experience = model.Experience,
                HourlyRateFrom = model.HourlyRateFrom,
                HourlyRateTo = model.HourlyRateTo,
                ProjectBudget = model.ProjectBudget,
                Description = model.Description,
                UserId = userId
            };

            _context.JobPosts.Add(jobPost);
            await _context.SaveChangesAsync();

            return RedirectToAction("Client_Dashboard");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
