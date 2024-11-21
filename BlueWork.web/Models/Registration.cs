using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BlueWork.web.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string Industry { get; set; }
        public byte ProfilePicture { get; set; }

        // Navigation properties (one-to-one relationships)
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<EmployerProfile> EmployerProfiles { get; set; }
        public virtual ICollection<WorkerProfile> WorkerProfiles { get; set; }
    }
}
