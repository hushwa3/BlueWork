using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class EmployerProfile
    {
        [Key]
        public int EmployerID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }

        [ForeignKey("RegistrationID")]
        public int RegistrationID { get; set; }
        // Navigation property (one-to-one relationship with Registration)
        public virtual ICollection<Registration> Registration { get; set; }

        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<JobListing> JobListings { get; set; }
        public virtual ICollection<SkillDevelopment> SkillDevelopment { get; set; }
    }
}
