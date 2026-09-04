namespace JobApplicationAPI.DTOs.JobApplications
{
    public record ManageJobApplicationRequest
    {
        public long ApplicationId { get; init; }
    }
}
