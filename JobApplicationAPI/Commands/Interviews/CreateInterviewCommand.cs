using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public record CreateInterviewCommand : IRequest<Unit>;
}
