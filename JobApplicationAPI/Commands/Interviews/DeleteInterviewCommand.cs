using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public record DeleteInterviewCommand : IRequest<Unit>;
}
