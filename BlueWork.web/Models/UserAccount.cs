using System.ComponentModel.DataAnnotations;

namespace BlueWork.web.Models
{
    public class UserAccount
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
