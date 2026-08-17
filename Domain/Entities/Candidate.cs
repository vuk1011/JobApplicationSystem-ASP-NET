namespace Domain.Entities
{
    public class Candidate : AbstractUser
    {
        public byte[] Resume { get; set; }
        public List<JobApplication> JobApplications { get; set; } = [];
    }
}
