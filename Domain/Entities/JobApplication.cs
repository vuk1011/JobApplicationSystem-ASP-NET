namespace Domain.Entities
{
    public class JobApplication
    {
        public long Id { get; set; }
        public DateOnly DateOfSubmission { get; set; }
        public JobApplicationStatus Status { get; set; }
        public long JobPostingId { get; set; }
        public JobPosting JobPosting { get; set; } = null!;
        public long? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public long CandidateId { get; set; }
        public Candidate Candidate { get; set; } = null!;
        public List<Offer> Offers { get; } = [];
        public List<Interview> Interviews { get; } = [];

        public bool IsManaged => Employee != null;
    }

    public enum JobApplicationStatus
    {
        SUBMITTED,
        UNDER_REVIEW,
        INTERVIEW_SCHEDULED,
        OFFERED,
        ACCEPTED,
        REJECTED,
    }

    public static class JobApplicationStatusUtil
    {
        public static bool IsStatusChangeAllowed(JobApplicationStatus before, JobApplicationStatus after)
        {
            if (before == after)
            {
                return true;
            }
            if (before == JobApplicationStatus.UNDER_REVIEW && (after == JobApplicationStatus.INTERVIEW_SCHEDULED || after == JobApplicationStatus.REJECTED))
            {
                return true;
            }
            else if (before == JobApplicationStatus.INTERVIEW_SCHEDULED && (after == JobApplicationStatus.OFFERED || after == JobApplicationStatus.REJECTED))
            {
                return true;
            }
            else if (before == JobApplicationStatus.OFFERED && (after == JobApplicationStatus.ACCEPTED || after == JobApplicationStatus.REJECTED))
            {
                return true;
            }
            else if (before == JobApplicationStatus.REJECTED && after == JobApplicationStatus.OFFERED)
            {
                return true;
            }
            else if (before == JobApplicationStatus.REJECTED && after == JobApplicationStatus.ACCEPTED)
            {
                return true;
            }
            return false;
        }
    }
}
