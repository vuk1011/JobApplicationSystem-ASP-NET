namespace Domain.Entities
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<Employee> Employees { get; } = [];
        public List<JobPosting> JobPostings { get; } = [];
    }
}
