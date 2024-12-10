namespace BlueWork.web.Models
{
    public class Hire
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string WorkerId { get; set; }

        // Navigation properties
        public JobPost JobPost { get; set; }
        public ApplicationUser Worker { get; set; }
    }

}
