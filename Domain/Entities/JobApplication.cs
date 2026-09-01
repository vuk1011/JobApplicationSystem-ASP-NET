namespace Domain.Entities
{
    public class JobApplication
    {
        public long Id { get; set; }
        public DateOnly DateSubmitted { get; set; }
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
        Submitted,
        UnderReview,
        InterviewScheduled,
        Offered,
        Accepted,
        Rejected,
    }

    public static class JobApplicationStatusUtil
    {
        public static bool IsStatusChangeAllowed(JobApplicationStatus before, JobApplicationStatus after)
        {
            if (before == after)
            {
                return true;
            }
            if (before == JobApplicationStatus.UnderReview && (after == JobApplicationStatus.InterviewScheduled || after == JobApplicationStatus.Rejected))
            {
                return true;
            }
            else if (before == JobApplicationStatus.InterviewScheduled && (after == JobApplicationStatus.Offered || after == JobApplicationStatus.Rejected))
            {
                return true;
            }
            else if (before == JobApplicationStatus.Offered && (after == JobApplicationStatus.Accepted || after == JobApplicationStatus.Rejected))
            {
                return true;
            }
            else if (before == JobApplicationStatus.Rejected && after == JobApplicationStatus.Offered)
            {
                return true;
            }
            else if (before == JobApplicationStatus.Rejected && after == JobApplicationStatus.Accepted)
            {
                return true;
            }
            return false;
        }
    }
}
