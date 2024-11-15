using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoginStatus { get; set; }
        public DateTime LastLogin { get; set; }

        [ForeignKey("RegistrationID")]
        public int RegistrationID { get; set; }
        // Navigation property (one-to-one relationship with Registration)
        public virtual ICollection<Registration> Registrations { get; set; }
        public virtual ICollection<EmployerProfile> EmployerProfiles { get; set; }
        public virtual ICollection<WorkerProfile> WorkerProfiles { get; set; }
    }
}
