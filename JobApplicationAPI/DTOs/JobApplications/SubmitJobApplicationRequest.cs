namespace JobApplicationAPI.DTOs.JobApplications
{
    public record SubmitJobApplicationRequest
    {
        public long JobPostingId { get; init; }
    }
}
