using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class JobListing
    {
        [Key]
        public int JobID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string RequiredSkills { get; set; }
        public string Location { get; set; }

        public string Complexity { get; set; }
        public string Duration { get; set; }
        public string Experience { get; set; }
        public Decimal BudgetFrom { get; set; }
        public Decimal BudgetTo { get; set; }
        public Decimal MaximumBudget { get; set; }
        public Decimal Salary { get; set; }

        [ForeignKey("EmployerID")]
        public int EmployerID { get; set; }
        // Navigation property (many-to-one relationship with EmployerProfile)
        public virtual ICollection<EmployerProfile> EmployerProfiles { get; set; }
        // One-to-many relationship with JobApplication
        public virtual ICollection<JobApplication> JobApplications { get; set; }
    }
}
