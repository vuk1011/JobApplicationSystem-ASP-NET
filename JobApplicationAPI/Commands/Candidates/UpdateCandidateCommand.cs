using JobApplicationAPI.DTOs.Users;
using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public record UpdateCandidateCommand(string? UserId, UpdateCandidateRequest Request) : IRequest<CandidateDto>;
}
