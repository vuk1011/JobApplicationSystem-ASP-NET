using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public record DeleteInterviewCommand(string? UserId, long InterviewId) : IRequest<Unit>;
}
