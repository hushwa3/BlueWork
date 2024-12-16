using Abp.Authorization.Users;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace BlueWork.web.Models
{
    public class WorkerProfile
    {
        [Key]
        public int WorkerID { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string LocationCity { get; set; }
        [Required(ErrorMessage = "Speciality is required")]
        public string Speciality { get; set; }
        [Required(ErrorMessage = "Please Input languages")]
        public string Languages { get; set; }
        [Required(ErrorMessage = "Education is required")]
        public string Education { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public string course { get; set; }
        [Required(ErrorMessage = "Graduation Year is required")]
        public int year { get; set; }
        [Required(ErrorMessage = "Please input hours per week")]
        public int HoursPerWeek { get; set; }
        [Required(ErrorMessage = "Select Offer Status")]
        public string Offer { get; set; }
        [Required(ErrorMessage = "Response time is required")]
        public int responseTime { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } // Navigation Property

    }
}