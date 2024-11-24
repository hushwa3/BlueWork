using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace BlueWork.web.BlueWorkAuth
{
    [Index(nameof(Email), IsUnique = true)]

    public class UserAccount
    {

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
    }
}
