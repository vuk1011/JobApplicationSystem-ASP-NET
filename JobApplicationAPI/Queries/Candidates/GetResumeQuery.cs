using MediatR;

namespace JobApplicationAPI.Queries.Candidates
{
    public record GetResumeQuery(string? UserId) : IRequest<byte[]>;
}
