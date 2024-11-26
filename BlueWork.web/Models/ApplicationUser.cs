using Microsoft.AspNetCore.Identity;

namespace BlueWork.web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<JobPost> JobPosts { get; set; }

    }
}
