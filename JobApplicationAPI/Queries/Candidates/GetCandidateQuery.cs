using JobApplicationAPI.DTOs.Users;
using MediatR;

namespace JobApplicationAPI.Queries.Candidates
{
    public record GetCandidateQuery(string? UserId) : IRequest<CandidateDto>;
}
