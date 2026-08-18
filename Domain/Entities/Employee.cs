namespace Domain.Entities
{
    public class Employee : GeneralUser
    {
        public string NationalId { get; set; } = string.Empty;
        public DateOnly DateBorn { get; set; }
        public DateOnly DateHired { get; set; }
        public List<JobApplication> ManagedJobApplications { get; set; } = [];
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
