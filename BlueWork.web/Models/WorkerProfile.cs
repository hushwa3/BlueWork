using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class WorkerProfile
    {
        [Key]
        public int WorkerID { get; set; }
        [Required]
        public int UserAccountId { get; set; }
        public string Bio { get; set; }
        public string Skills { get; set; }
        public int ExperienceYears { get; set; }
        public decimal Rating { get; set; }


        // One-to-many relationship with JobApplication
        public virtual ICollection<JobApplication> JobApplications { get; set; }

        public virtual ICollection<SkillDevelopment> SkillDevelopments { get; set; }
    }
}