using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class ClientProfile
    {
        [Key]
        public int ClientProfileId { get; set; }

        [Required(ErrorMessage = "Client Name is required")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Company Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Company Size is required")]
        public string CompanySize { get; set; }

        [Required(ErrorMessage = "Industry is required")]
        public string Industry { get; set; }

        public bool PaymentVerification { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
