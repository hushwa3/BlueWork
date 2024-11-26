using BlueWork.web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BlueWork.web.Models;

namespace BlueWork.web.Models
{ 
public class AddPostModel : PageModel
{
    private readonly BlueWorkDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AddPostModel(BlueWorkDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public AddPostModel() { }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        public string Headline { get; set; }

        [Required]
        public List<string> Skills { get; set; }

        [Required]
        public string Complexity { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string Experience { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal HourlyRateFrom { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal HourlyRateTo { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal ProjectBudget { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }

    public void OnGet()
    {
        Input = new InputModel();
    }
}
}