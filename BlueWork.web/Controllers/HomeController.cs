using BlueWork.web.Data;
using BlueWork.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace BlueWork.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlueWorkDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Injecting dependencies
        public HomeController(BlueWorkDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager=userManager;
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

        [HttpGet]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> EditProfile()
        {
            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Find the existing worker profile
            var workerProfile = await _context.WorkerProfiles
                .FirstOrDefaultAsync(w => w.UserId == user.Id);

            // If no profile exists, return a not found error
            if (workerProfile == null)
            {
                return NotFound("Worker profile not found.");
            }

            return View(workerProfile);

        }

        [HttpPost]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> EditProfile(WorkerProfile workerProfile)
        {
            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Explicitly set the UserId to the current user's ID
            workerProfile.UserId = user.Id;

            // Remove the UserId field from ModelState validation
            ModelState.Remove("UserId");

            // Validate model state
            if (!ModelState.IsValid)
            {
                return View(workerProfile);
            }

            try
            {
                // Find the existing worker profile
                var existingProfile = await _context.WorkerProfiles
                    .FirstOrDefaultAsync(w => w.UserId == user.Id);

                if (existingProfile == null)
                {
                    return NotFound("Worker profile not found.");
                }

                // Manually update all properties
                existingProfile.LocationCity = workerProfile.LocationCity;
                existingProfile.Speciality = workerProfile.Speciality;
                existingProfile.Languages = workerProfile.Languages;
                existingProfile.Education = workerProfile.Education;
                existingProfile.course = workerProfile.course;
                existingProfile.year = workerProfile.year;
                existingProfile.HoursPerWeek = workerProfile.HoursPerWeek;
                existingProfile.Offer = workerProfile.Offer;
                existingProfile.responseTime = workerProfile.responseTime;

                // Explicitly update the entity
                _context.WorkerProfiles.Update(existingProfile);

                // Save changes
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("WorkerProfile");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error updating profile: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");

                ModelState.AddModelError(string.Empty, "An error occurred while updating the profile.");
                return View(workerProfile);
            }

        }
        [HttpGet]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> WorkerProfile()
        {
            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Check if a WorkerProfile exists
            var workerProfile = await _context.WorkerProfiles
                .FirstOrDefaultAsync(w => w.UserId == user.Id);

            if (workerProfile == null)
            {
                // Create a default WorkerProfile with placeholder values
                workerProfile = new WorkerProfile
                {
                    UserId = user.Id,
                    LocationCity = "To be edited",
                    Speciality = "To be edited",
                    Languages = "To be edited",
                    Education = "To be edited",
                    course="To be edited",
                    year=2000,
                    HoursPerWeek = 0,
                    Offer = "To be edited",
                    responseTime = 0
                };

                // Save the default profile to the database
                _context.WorkerProfiles.Add(workerProfile);
                await _context.SaveChangesAsync();
            }

            // Pass FirstName and LastName to the view using ViewData
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;

            // Pass the WorkerProfile to the view
            return View(workerProfile);

        }

        [HttpGet]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> ApplyJob(int id)
        {
            // Fetch the job post with the related user data
            var jobPost = await _context.JobPosts.FirstOrDefaultAsync(j => j.Id == id);

            if (jobPost == null)
            {
                return NotFound();
            }

            // Pass any success message from TempData (after ApplyToJob)
            ViewBag.Message = TempData["Message"];

            return View(jobPost);
        }

        [HttpPost]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> ApplyToJob(int id)
        {
            // Get the current user's information
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                TempData["Message"] = "An error occurred while applying for the job.";
                return RedirectToAction("ApplyJob", new { id });
            }

            var firstName = user.FirstName;
            var lastName = user.LastName;

            // File path for storing applicants
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "JSON", "applicants.json");

            // Read existing data
            var applicants = new List<JsonElement>();
            if (System.IO.File.Exists(filePath))
            {
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                applicants = JsonSerializer.Deserialize<List<JsonElement>>(json) ?? new List<JsonElement>();
            }

            // Check if the user has already applied for this job
            if (applicants.Any(a =>
            {
                var jobId = a.GetProperty("JobId").GetInt32();
                var applicantUserId = a.GetProperty("UserId").GetString();
                return jobId == id && applicantUserId == userId;
            }))
            {
                TempData["Message"] = "You have already applied for this job.";
                return RedirectToAction("ApplyJob", new { id });
            }

            // Add the new applicant
            var newApplicant = new
            {
                JobId = id,
                UserId = userId,
                FirstName = firstName,
                LastName = lastName
            };

            // Serialize and write back to file
            applicants.Add(JsonSerializer.SerializeToElement(newApplicant));
            var updatedJson = JsonSerializer.Serialize(applicants);
            await System.IO.File.WriteAllTextAsync(filePath, updatedJson);

            TempData["Message"] = "You have successfully applied for the job.";
            return RedirectToAction("ApplyJob", new { id });
        }


        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ReviewProposal(int jobId)
        {
            // Path to the JSON file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "JSON", "applicants.json");

            // Initialize applicants list
            var applicants = new List<JsonElement>();

            // Read and parse JSON file
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    var json = await System.IO.File.ReadAllTextAsync(filePath);
                    applicants = JsonSerializer.Deserialize<List<JsonElement>>(json) ?? new List<JsonElement>();
                }
                catch (JsonException)
                {
                    // Handle invalid JSON by initializing an empty list
                    applicants = new List<JsonElement>();
                }
            }

            // Filter applicants for the specific JobId
            var jobApplicants = applicants
                .Where(a =>
                {
                    try
                    {
                        // Safely access and compare the JobId
                        return a.TryGetProperty("JobId", out var jobIdProperty) && jobIdProperty.GetInt32() == jobId;
                    }
                    catch
                    {
                        return false; // Skip invalid entries
                    }
                })
                .ToList();

            // Pass filtered applicants and JobId to the view
            ViewBag.Applicants = jobApplicants;
            ViewBag.JobId = jobId;

            return View();
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

        public IActionResult Hire()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
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

        public IActionResult JobPostSuccess()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
