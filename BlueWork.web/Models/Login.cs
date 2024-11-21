using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")];
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength= 5, ErrorMessage = "Min 5 or Max 20 characters allowed.")]
        public string Password { get; set; }

        [ForeignKey("RegistrationID")]
        public int RegistrationID { get; set; }
        // Navigation property (one-to-one relationship with Registration)
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<EmployerProfile> EmployerProfiles { get; set; }
        public virtual ICollection<WorkerProfile> WorkerProfiles { get; set; }
    }
}
