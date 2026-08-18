namespace Domain.Entities
{
    public class Candidate : GeneralUser
    {
        public byte[] Resume { get; set; }
        public List<JobApplication> JobApplications { get; set; } = [];
    }
}
