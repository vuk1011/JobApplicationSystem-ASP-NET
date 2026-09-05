using JobApplicationAPI.DTOs.Interviews;
using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public record CreateInterviewCommand(string? UserId, CreateInterviewRequest Request) : IRequest<Unit>;
}
