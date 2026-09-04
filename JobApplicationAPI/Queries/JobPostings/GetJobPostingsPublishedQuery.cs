using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public record GetJobPostingsPublishedQuery : IRequest<Unit>;
}
