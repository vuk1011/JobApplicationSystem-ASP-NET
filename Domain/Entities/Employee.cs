namespace Domain.Entities
{
    public class Employee : GeneralUser
    {
        public string NationalId { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public DateOnly DateOfHire { get; set; }
        public List<JobApplication> ManagedJobApplications { get; } = [];
        public long CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
