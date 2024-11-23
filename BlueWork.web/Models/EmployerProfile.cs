using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class EmployerProfile
    {
        [Key]
        public int EmployerID { get; set; }
        [Required]
        public int UserAccountId { get; set; }
        public string FirstName { get; set; }  
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int ContactNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
      

        [ForeignKey("RegistrationID")]
        public int RegistrationID { get; set; }
        // Navigation property (one-to-one relationship with Registration)
        public virtual ICollection<UserAccount> UserAccounts { get; set; }

        public virtual ICollection<Login> Login { get; set; }
        public virtual ICollection<JobListing> JobListings { get; set; }
        public virtual ICollection<SkillDevelopment> SkillDevelopment { get; set; }
    }
}
