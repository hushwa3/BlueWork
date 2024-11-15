using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class JobApplication
    {
        [Key]
        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string ApplicationDescription { get; set; }
        public string Industry { get; set; }

        [ForeignKey("JobID")]
        public int JobID { get; set; }
        [ForeignKey("WorkerID")]
        public string WorkerID { get; set; }
        // Navigation properties (many-to-one relationships)
        public virtual ICollection<JobListing> JobListings { get; set; }
        public virtual ICollection<WorkerProfile> WorkerProfiles { get; set; }
    }
}
