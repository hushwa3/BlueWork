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
        private readonly ILogger<HomeController> _logger;


        // Injecting dependencies
        public HomeController(BlueWorkDbContext context, UserManager<ApplicationUser> userManager,
             ILogger<HomeController> logger)
        {
            _context = context;
            _userManager=userManager;
            _logger = logger;

        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Join()
        {
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ClientInfoProfile()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var clientProfile = await _context.ClientProfiles
                    .FirstOrDefaultAsync(c => c.UserId == user.Id);

                if (clientProfile == null)
                {
                    return RedirectToAction("EditClientProfile");
                }

                // Pass user data to view
                ViewData["FirstName"] = user.FirstName ?? "N/A";
                ViewData["LastName"] = user.LastName ?? "N/A";

                // Display success message if present
                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
                }

                return View(clientProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ClientInfoProfile: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while loading the profile.";
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> EditClientProfile()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var profile = await _context.ClientProfiles
                    .FirstOrDefaultAsync(c => c.UserId == user.Id);

                if (profile == null)
                {
                    profile = new ClientProfile
                    {
                        UserId = user.Id,
                        ClientName = $"{user.FirstName ?? ""} {user.LastName ?? ""}".Trim(),
                        Email = user.Email,
                        CompanyName = "",
                        Location = "",
                        Description = "",
                        CompanySize = "",
                        Industry = "",
                        PaymentVerification = false
                    };
                }

                return View(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in EditClientProfile: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while loading the profile.";
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> EditClientProfile(ClientProfile model)
        {
            try
            {
                // Log the incoming model
                _logger.LogInformation($"Received model: {model.ClientName}, {model.Email}, UserId: {model.UserId}");

                // Validate the model
                if (!ModelState.IsValid)
                {
                    foreach (var modelStateKey in ModelState.Keys)
                    {
                        var value = ModelState[modelStateKey];
                        foreach (var error in value.Errors)
                        {
                            _logger.LogError($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                        }
                    }
                    return View(model);
                }

                // Fetch the logged-in user
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "User not found. Please log in.";
                    return RedirectToAction("Login", "Account");
                }

                // Log the user details
                _logger.LogInformation($"Current UserId: {user.Id}");

                // Fetch existing profile
                var existingProfile = await _context.ClientProfiles
                    .FirstOrDefaultAsync(c => c.UserId == user.Id);

                if (existingProfile == null)
                {
                    // Create a new profile
                    var newProfile = new ClientProfile
                    {
                        UserId = user.Id,
                        ClientName = model.ClientName,
                        Email = model.Email,
                        CompanyName = model.CompanyName,
                        Location = model.Location,
                        Description = model.Description,
                        CompanySize = model.CompanySize,
                        Industry = model.Industry,
                        PaymentVerification = model.PaymentVerification
                    };

                    await _context.ClientProfiles.AddAsync(newProfile);
                    TempData["SuccessMessage"] = "Profile created successfully!";
                }
                else
                {
                    // Update existing profile
                    existingProfile.ClientName = model.ClientName;
                    existingProfile.Email = model.Email;
                    existingProfile.CompanyName = model.CompanyName;
                    existingProfile.Location = model.Location;
                    existingProfile.Description = model.Description;
                    existingProfile.CompanySize = model.CompanySize;
                    existingProfile.Industry = model.Industry;
                    existingProfile.PaymentVerification = model.PaymentVerification;

                    _context.ClientProfiles.Update(existingProfile);
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                }

                // Save changes and log result
                await _context.SaveChangesAsync();
                _logger.LogInformation("Changes saved successfully to the database.");

                return RedirectToAction(nameof(ClientInfoProfile));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving profile: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving the profile.");
                return View(model);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ViewWorkerProfile(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound("Worker ID is required.");
            }

            // Path to the JSON file
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "JSON", "applicants.json");

            // Initialize applicants list
            var applicants = new List<JsonElement>();

            // Read and parse the JSON file
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

            // Find the applicant matching the provided UserId
            var applicant = applicants.FirstOrDefault(a =>
            {
                try
                {
                    return a.TryGetProperty("UserId", out var userIdProperty) && userIdProperty.GetString() == id;
                }
                catch
                {
                    return false; // Skip invalid entries
                }
            });

            if (applicant.ValueKind == JsonValueKind.Undefined)
            {
                return NotFound("Worker profile not found in JSON.");
            }

            // Use the WorkerProfile model for additional data
            var workerProfile = await _context.WorkerProfiles
                .FirstOrDefaultAsync(w => w.UserId == id);

            if (workerProfile == null)
            {
                return NotFound("Worker profile not found in the database.");
            }

            // Optionally, add JSON data to ViewBag
            ViewBag.ApplicantData = applicant;
            ViewBag.FirstName = applicant.GetProperty("FirstName").GetString();
            ViewBag.LastName = applicant.GetProperty("LastName").GetString();

            return View(workerProfile);
        }


        public IActionResult WorkerProfile_Setup()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> EditProfile()
        {
            try
            {
                // Get the logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Find the existing worker profile or create a default one
                var workerProfile = await _context.WorkerProfiles
                    .FirstOrDefaultAsync(w => w.UserId == user.Id);

                if (workerProfile == null)
                {
                    workerProfile = new WorkerProfile
                    {
                        UserId = user.Id,
                        LocationCity = "To be edited",
                        Speciality = "To be edited",
                        Languages = "To be edited",
                        Education = "To be edited",
                        course = "To be edited",
                        year = DateTime.Now.Year,
                        HoursPerWeek = 0,
                        Offer = "To be edited",
                        responseTime = 0
                    };

                    _context.WorkerProfiles.Add(workerProfile);
                    await _context.SaveChangesAsync();
                }

                return View(workerProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditProfile GET: {ex.Message}");
                return View("Error");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Worker")]
        public async Task<IActionResult> EditProfile(WorkerProfile workerProfile)
        {
            try
            {
                // Get the logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Ensure the UserId is set correctly
                workerProfile.UserId = user.Id;

                // Remove UserId validation
                ModelState.Remove(nameof(workerProfile.UserId));

                if (!ModelState.IsValid)
                {
                    return View(workerProfile);
                }

                // Find the existing profile
                var existingProfile = await _context.WorkerProfiles
                    .FirstOrDefaultAsync(w => w.UserId == user.Id);

                if (existingProfile == null)
                {
                    return NotFound("Worker profile not found.");
                }

                // Update properties
                existingProfile.LocationCity = workerProfile.LocationCity;
                existingProfile.Speciality = workerProfile.Speciality;
                existingProfile.Languages = workerProfile.Languages;
                existingProfile.Education = workerProfile.Education;
                existingProfile.course = workerProfile.course;
                existingProfile.year = workerProfile.year;
                existingProfile.HoursPerWeek = workerProfile.HoursPerWeek;
                existingProfile.Offer = workerProfile.Offer;
                existingProfile.responseTime = workerProfile.responseTime;

                _context.WorkerProfiles.Update(existingProfile);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("WorkerProfile");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditProfile POST: {ex.Message}");
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
                        // Extract and compare JobId safely
                        return a.TryGetProperty("JobId", out var jobIdProperty) && jobIdProperty.GetInt32() == jobId;
                    }
                    catch
                    {
                        return false; // Skip invalid entries
                    }
                })
                .Select(a => new
                {
                    JobId = a.GetProperty("JobId").GetInt32(),
                    UserId = a.GetProperty("UserId").GetString(),
                    FirstName = a.GetProperty("FirstName").GetString(),
                    LastName = a.GetProperty("LastName").GetString()
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
