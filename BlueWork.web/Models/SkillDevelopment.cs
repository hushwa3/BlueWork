using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueWork.web.Models
{
    public class SkillDevelopment
    {
        [Key]
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }
        public string TrainingProvider { get; set; }
        public int TrainingDuration { get; set; }

        [ForeignKey("WorkerID")]
        public int WorkerID { get; set; }
        [ForeignKey("EmploiyerID")]
        public int EmployerID { get; set; }    

        public virtual ICollection<WorkerProfile> WorkerProfiles { get; set; }
        public virtual ICollection<EmployerProfile> EmployerProfiles { get; set; }
    }
}
