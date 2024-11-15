using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlueWork.web.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // Navigation properties (one-to-one relationships)
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<EmployerProfile> EmployerProfiles { get; set; }
        public virtual ICollection<WorkerProfile> WorkerProfiles { get; set; }
    }
}
