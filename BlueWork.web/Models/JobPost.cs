using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class JobPost
    {
        [Key]
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Skills { get; set; }
        public string Complexity { get; set; }
        public string Duration { get; set; }
        public string Experience { get; set; }
        public decimal HourlyRateFrom { get; set; }
        public decimal HourlyRateTo { get; set; }
        public decimal ProjectBudget { get; set; }
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }

    }

}
